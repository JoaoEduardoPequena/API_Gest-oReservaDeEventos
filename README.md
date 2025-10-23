# 📬 API Message Pub/Sub com Redis e ASP.NET Core

Este projecto demonstra um fluxo assíncrono de pedidos de restaurante usando **Redis Pub/Sub**, **ASP.NET Core 8**, **Clean Architecture** e **Background Services**.

---

## 📌 Visão Geral

Imagine o seguinte cenário:

Um cliente realiza um pedido (nome, e-mail e descrição do pedido).  
A API salva o pedido no banco de dados e publica uma mensagem no Redis.  
Um serviço em segundo plano escuta esse canal e envia um e-mail de confirmação ao cliente — tudo de forma assíncrona e desacoplada.

---

## 🔄 Fluxo de Funcionamento

1. **Cliente** envia `POST /api/pedidos`
2. **API**:
   - Salva o pedido no banco via EF Core
   - Publica a mensagem no canal Redis `channel-pedido-novos`
3. **NotificadorPedidos.Worker**:
   - Escuta o canal Redis
   - Ao receber a mensagem, envia um e-mail de confirmação ao cliente

---

## 🧠 Quando Usar Redis Pub/Sub?

✅ Use quando:
- Precisa de comunicação em tempo real
- Não é necessário armazenar ou reprocessar mensagens
- Deseja baixo acoplamento entre serviços

❌ Evite quando:
- Precisa de persistência ou confirmação de entrega
- Precisa de reprocessamento ou tolerância a falhas  
👉 Nesse caso, considere usar: **RabbitMQ**, **Kafka** ou **Azure Service Bus**

---

## ⚙️ Tecnologias Utilizadas

- [ASP.NET Core 8]
- Clean Architecture
- [Redis](https://redis.io/) (Pub/Sub)
- CQRS + MediatR
- SQL Server + Entity Framework Core
- BackgroundService com Redis Listener
