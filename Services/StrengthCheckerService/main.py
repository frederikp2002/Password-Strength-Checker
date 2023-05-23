# Import the neccessary Python libraries and the dataset we need 
import pandas as pd
import numpy as np
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier
import getpass
import os
import pickle
import bcrypt
import pika
import json
from datetime import datetime 

# Function used to tokenize the passwords that are in the dataset!
# We do this because we need the model to learn from the combination of characters (digits, letters, symbols) to predict the password's strength
def word(password):
    character=[]
    for i in password:
        character.append(i)
    return character

# Write a function that creates a connection and a channel and a queue in RabbitMQ
def create_connection():
    connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost'))
    channel = connection.channel()
    channel.queue_declare(queue='password_queue')
    return channel

# This function is used to publish the message to the RabbitMQ queue
# The message is the password that the user has entered as well as the password's strength - The date and time is also included
def publish_message(message):
    connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost'))
    channel = connection.channel()
    channel.queue_declare(queue='password_queue')
    print("Password hash and strength to database... Done!")

    channel.basic_publish(exchange='', 
                            routing_key='password_queue', 
                            body=json.dumps(message))
    connection.close()

# If it has, load the model and TfidVectorizer from disk
# If it hasn't, train the model and save it to disk
if os.path.exists("model.pkl"):
    print("Loading model from disk... ")
    with open('model.pkl', 'rb') as f:
        model, tdif = pickle.load(f)
else:
    print("Training model...")
    data = pd.read_csv("Data\shortdata.csv", error_bad_lines=False)

    # The numbers 0, 1 and 2 means "weak", "medium" and "strong" respectively
    # We will map the numbers to the corresponding words
    data = data.dropna()
    data["strength"] = data["strength"].map({
        0: "Weak", 
        1: "Medium",
        2: "Strong"
        })

    # We will now split the dataset into training and testing data
    x = np.array(data["password"])
    y = np.array(data["strength"])
    print("tokenizing...")

    tdif = TfidfVectorizer(tokenizer=word)
    x = tdif.fit_transform(x)
    xtrain, xtest, ytrain, ytest = train_test_split(x, y, test_size=0.05, random_state=42)
    print("training...")

    # We will now train the model using the Random Forest Classifier
    model = RandomForestClassifier()
    print("testing... - this may take a while depending on the size of the dataset and the speed of your computer")
    model.fit(xtrain, ytrain)
    print("accuracy ", model.score(xtest, ytest))

    # Save the model to disk 
    with open("model.pkl", "wb") as f:
        pickle.dump((model, tdif), f)

# We will now ask the user to input a password and the MachineLearning model will predict the password's strength
print("Enter a password to test its strength")
user = getpass.getpass("Enter Password: ")
data = tdif.transform([user]).toarray()
strength = model.predict(data)
print("The password's strength is:", strength[0])

# Hash the password and add a salt to it
salt = bcrypt.gensalt()
hashed_password = bcrypt.hashpw(user.encode('utf-8'), salt)

# Get current date and time
passwordGenerationDate = datetime.today().strftime('%Y-%m-%d')

# Publish the message to the RabbitMQ queue 
# The message is the password that the user has entered as well as the password's strength - The date and time is also included
publish_message({"PasswordHash": hashed_password.decode('utf-8'), "PasswordStrength": strength[0], "PasswordDateTime": passwordGenerationDate})