# Request Logging System

## Overview

This project implements a distributed logging system that captures incoming HTTP requests, logs relevant metadata, and stores the data in a PostgreSQL database. The system consists of multiple components, including a web API, middleware for request logging, a RabbitMQ-based message broker, and a consumer that processes log messages asynchronously.

## Components

### 1. Web API

The core of the system is a .NET Web API that exposes endpoints to receive and retrieve logs. It interacts with a PostgreSQL database using Entity Framework Core and provides the following functionalities:

POST /api/logs: Accepts log data from RabbitMQ and stores it in the database.

GET /api/logs: Retrieves logs based on filtering criteria, such as RequestId, RouteURL, or a date range.

### 2. Request Logging Middleware

A custom middleware captures incoming HTTP requests and logs relevant information before passing the request to the next middleware. This middleware extracts the request method, route, query string, headers, and response time. It then sends this data to RabbitMQ for processing.

Key Features:

Captures request details

Measures request processing time

Publishes log entries to RabbitMQ

### 3. RabbitMQ Message Broker

RabbitMQ acts as an intermediary between the web API and the logging system. The middleware publishes log messages to a queue, allowing them to be processed asynchronously by a separate consumer.

Key Features:

Decouples log generation from storage

Ensures reliable message delivery

Supports scaling by adding more consumers

### 4. RabbitMQ Consumer

A background service that listens to messages from RabbitMQ and forwards them to the Web API’s logging endpoint. This ensures logs are processed in an asynchronous manner, reducing the load on the main application.

Key Features:

Listens for incoming log messages

Sends log data to the Web API

Runs as a background service in .NET

### 5. Database Storage

Logs are persisted in a PostgreSQL database using Entity Framework Core. The schema includes indexes on frequently queried fields (RequestId, Timestamp) to optimize retrieval performance.

Database Schema:

Id: Primary key (BigSerial)

RequestId: Unique identifier (UUID)

RequestObject: Stores request payload (JSONB)

RouteURL: The accessed URL (TEXT)

Timestamp: Request timestamp (TIMESTAMP WITH TIME ZONE)

### 6. RabbitMQ Publisher

A separate .NET service that allows other applications to publish log messages to RabbitMQ. This enables integration with multiple services that need to send log data.

Key Features:

Simple API for sending log data

Publishes messages to RabbitMQ queue

Designed to be used by different applications
