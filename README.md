## 🎟️ API de Reserva de Eventos com Redis e RabbitMQ

Este projeto demonstra um fluxo assíncrono de pedidos de reserva de eventos utilizando Redis, RabbitMQ, ASP.NET Core 8, CQRS, MediatR,Clean Architecture e Background Services.

## 📌 Visão Geral

Imagine o seguinte cenário:

Um utilizador realiza um pedido de reserva para um evento (nome, e-mail, ID do evento e quantidade de bilhetes).
A API recebe o pedido, armazena os dados temporariamente no Redis e publica uma mensagem no RabbitMQ.
Um serviço em segundo plano (Worker Service) consome essa mensagem, gera uma ficha de confirmação da reserva e envia um e-mail de confirmação ao cliente — tudo de forma assíncrona e desacoplada.

## 🔄 Fluxo de Funcionamento

Cliente envia POST /api/reservas

## API:

Valida e armazena temporariamente os dados da reserva no Redis

Publica a mensagem no RabbitMQ (queue: evento-reserva-criada)

## ProcessamentoReserva.Worker:

Consome as mensagens publicadas na fila do RabbitMQ

Gera a ficha de confirmação da reserva (PDF) e Envia um e-mail de confirmação ao cliente

## 🧠 Quando Usar RabbitMQ?

✅ Use quando:

Precisa de garantia de entrega da mensagem

Deseja processar tarefas em background de forma confiável

Quer desacoplar a API do processamento pesado (ex: geração de PDFs, envio de e-mails)

O sistema precisa de escalabilidade e resiliência

## ❌ Evite quando:

Precisa apenas de notificações rápidas e temporárias

Não é necessário reprocessar mensagens
👉 Nesse caso, Redis Pub/Sub pode ser uma opção mais simples e leve

## ⚙️ Tecnologias Utilizadas

ASP.NET Core 8

Redis
 – Cache e armazenamento temporário

RabbitMQ
 – Mensageria assíncrona

MassTransit
 – Integração com RabbitMQ

MailKit
 – Envio de e-mails

CQRS + MediatR

Clean Architecture

Worker Service (BackgroundService)

Docker + Docker Compose
