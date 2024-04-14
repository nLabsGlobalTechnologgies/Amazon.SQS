// Amazon SQS işlevlerini kullanmak için gerekli using ifadesi.
// Necessary using statement for using Amazon SQS functions.
using Amazon.SQS;
// Amazon SQS modellerini kullanmak için gerekli using ifadesi.
// Necessary using statement for using Amazon SQS models.
using Amazon.SQS.Model;
// JSON serileştirme/deserileştirme için gerekli using ifadesi.
// Necessary using statement for JSON serialization/deserialization.
using System.Text.Json;

// Amazon SQS ile bağlantı kurmak için gereken kodlar.
// Codes required to establish a connection with Amazon SQS.

// Yerel yapılandırmada tanımlı kimlik bilgileriyle Amazon SQS'ye bağlanmak için kullanılır.
// Used to connect to Amazon SQS with locally configured credentials.

// var sqsClient = new AmazonSQSClient();

// Manuel bağlanma seçeneği:
// Manual connection option:

// Erişim anahtarı (Access Key) bilgisini saklar.
// Stores the Access Key.
var accessKey = string.Empty;

// Gizli anahtar (Secret Key) bilgisini saklar.
// Stores the Secret Key.
var secretKey = string.Empty;

// Kullanılacak bölgenin tanımlanması.
// Defines the region to be used.
var region = Amazon.RegionEndpoint.EUWest3;

// SQS istemcisini oluşturur.
// Creates the SQS client.
var sqsClient = new AmazonSQSClient(accessKey, secretKey, region);

// Gönderilecek müşteri verileri için örnek bir nesne oluşturulur.
// Creates an example object for the customer data to be sent.
var customer = new 
{
    FirstName = "Cuma",
    LastName = "KÖSE",
    Age = 37
};

// Mesaj gönderme işleminin başladığını belirtir.
// Indicates the start of the message sending process.
Console.WriteLine(" [*] Starting sending queue message...");

// İlgili kuyruğun URL'sini almak için bir istek yapılır.
// Makes a request to get the URL of the relevant queue.
var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

// Gönderilecek mesajı temsil eden bir SendMessageRequest örneği oluşturulur.
// Creates an instance of SendMessageRequest to represent the message to be sent.
var sendMessageRequest = new SendMessageRequest
{
    // Kuyruk URL'si atanır.
    // Assigns the queue URL.
    QueueUrl = queueUrlResponse.QueueUrl,

    // Müşteri bilgileri JSON formatına dönüştürülerek mesaj gövdesine eklenir.
    // Adds customer data to the message body in JSON format.
    MessageBody = JsonSerializer.Serialize(customer),

    // Mesaj öznitelikleri belirlenir.
    // Specifies the message attributes.
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            // MessageType özniteliği oluşturulur. 1 den fazla oluşturabiliriz 
            // Creates the MessageType attribute.
            "MessageType", new MessageAttributeValue
            {
                // Veri türü belirlenir.
                // Specifies the data type.
                DataType = "String",

                // Öznitelik değeri atanır.
                // Assigns the attribute value.
                StringValue = "Customer"
            }
        },
        {
            // MessageType2 özniteliği oluşturulur. istemezsek oluşturmayabiliriz.            
            // Creates the MessageType2 attribute. Assigns the attribute value.
            "MessageType2", new MessageAttributeValue
            {
                 // Veri türü belirlenir.
                // Specifies the data type.
                DataType = "String2",

                // Öznitelik değeri atanır.
                // Assigns the attribute value.
                StringValue = "Customer2"
            }
        }
    }
};

// Oluşturulan mesaj kuyruğa gönderilir.
// Sends the created message to the queue.
var response = await sqsClient.SendMessageAsync(sendMessageRequest);

// Mesaj gönderme işleminin tamamlandığını belirtir.
// Indicates that the message sending process is completed.
Console.WriteLine(" [*] Send queue message is completed...");

// Programın kapanmasını önlemek için bir giriş bekler.
// Waits for an input to prevent the program from closing.
Console.ReadLine();