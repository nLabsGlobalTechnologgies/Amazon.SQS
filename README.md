# SQS SNS

## SQS Message Receiver

This code snippet demonstrates how to receive and process messages from an Amazon Simple Queue Service (SQS) queue using the AWS SDK for .NET.

### Code Explanation

- **Establish Connection**: The code establishes a connection with the SQS service using the `AmazonSQSClient` class.

- **Get Queue URL**: It retrieves the URL of the specified queue named "customers" using the `GetQueueUrlAsync` method.

- **Receive Messages**: The code creates a `ReceiveMessageRequest` object to specify the parameters for receiving messages from the queue, such as queue URL, attribute names, and message attribute names. It then continuously receives messages from the queue using a loop until a cancellation is requested.

- **Process Messages**: Each received message is processed inside the loop. The code prints the message ID and body to the console and then deletes the message from the queue using the `DeleteMessageAsync` method.

### Requirements

- AWS SDK for .NET
- Amazon SQS queue named "customers"
- Access to the SQS queue

### Usage

1. Ensure you have the necessary AWS credentials configured or provide them manually in the code.
2. Replace `"customers"` with the name of your SQS queue.
3. Run the code to start receiving and processing messages from the SQS queue.

### Notes

- This code assumes that you have already set up an SQS queue named "customers" and have the necessary permissions to access it.

