// Amazon SQS işlevlerini kullanmak için gerekli using ifadesi. 
// Necessary using statement for using Amazon SQS functions.
using Amazon.SQS;
// Amazon SQS modellerini kullanmak için gerekli using ifadesi. 
// Necessary using statement for using Amazon SQS models.
using Amazon.SQS.Model;

// SQS istemcisini oluşturur. 
// Creates the SQS client.
var sqsClient = new AmazonSQSClient();

// Belirtilen kuyruğun URL'sini alır. 
// Gets the URL of the specified queue.
var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

// Alınacak mesajı temsil eden bir ReceiveMessageRequest örneği oluşturur. 
// Creates an instance of ReceiveMessageRequest representing the message to be received.
var receiveMessageRequest = new ReceiveMessageRequest
{
    // Kuyruk URL'sini atar. 
    // Assigns the queue URL.
    QueueUrl = queueUrlResponse.QueueUrl,

    // Tüm özniteliklerin alınmasını sağlar. 
    // Ensures all attributes are received.
    AttributeNames = new List<string> { "All" },

    // Belirtilen mesaj özniteliklerini alır. 
    // Receives the specified message attributes.
    MessageAttributeNames = new List<string>() { "MessageType", "All" }
};

// İptal belirtecini oluşturur. 
// Creates a cancellation token source.
var cts = new CancellationTokenSource();

while (!cts.IsCancellationRequested)
{
    // Mesajı alır. 
    // Receives the message.
    var response = await sqsClient.ReceiveMessageAsync(receiveMessageRequest);
    foreach (var message in response.Messages)
    {
        //Mail > sending action / gönderme işlemi
        //SMS > sending action / gönderme işlemi
        try
        {
            // Mesajın Kimlik bilgisini yazdırır. 
            // Writes the Message Id.
            Console.WriteLine($"Message Id: {message.MessageId}");

            // Mesajın gövdesini yazdırır. 
            // Writes the Message Body.
            Console.WriteLine($"Message body: {message.Body}");

            // Alınan mesajı kuyruktan siler. 
            // Deletes the received message from the queue.
            await sqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle);
        }
        catch (Exception ex)
        {
            // Hata durumunda hata mesajını yazdırır. 
            // Writes the error message in case of exception.
            Console.WriteLine(ex.Message);
        }
    }

    // Belirtilen süre kadar bekler. 
    // Waits for the specified duration.
    await Task.Delay(100);
}