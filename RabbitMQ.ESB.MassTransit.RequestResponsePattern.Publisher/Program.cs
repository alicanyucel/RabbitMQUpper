﻿using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.RequestResponseMessages;

Console.WriteLine("Publisher");

string rabbitMQUri = "https://www.cloudamqp.com/";

string requestQueue = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

await bus.StartAsync();

var request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMQUri}/{requestQueue}"));

int i = 1;
while (true)
{
    await Task.Delay(200);
    var response = await request.GetResponse<ResponseMessage>(new() { MessageNo = i, Text = $"{i++}. request" });
    Console.WriteLine($"Response Received : {response.Message.Text}");
}

Console.Read();