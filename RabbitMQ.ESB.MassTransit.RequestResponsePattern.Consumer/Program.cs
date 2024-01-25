using MassTransit;
using RabbitMQ.ESB.MassTransit.RequestResponsePattern.Consumer.Consumers;

Console.WriteLine("Consumer");

string rabbitMQUri = "https://www.cloudamqp.com/";

string requestQueue = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);

    factory.ReceiveEndpoint(requestQueue, endpoint =>
    {
        endpoint.Consumer<ReqeustMessageConsumer>();
    });
});

await bus.StartAsync();

Console.Read();