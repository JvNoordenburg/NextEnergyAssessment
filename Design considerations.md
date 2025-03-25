# Design considerations

The design focuses on fully embracing Azure cloud, minimizing active processes during idle times and decoupled, asynchronous processing through a event-driven, serverless architecture. 

The design consists of the following components:
- *Imbalance Ingester* - Cron job triggered very minute. Only responsible for ensuring the new imbalance data is processed and stored in the imbalance DB
- *Imbalance DB* - Simple Relation Database. As per the context provided, it doesn't seem to require complex triggers or stored procedures and is just a simple store for imbalance data, thus a fully fledged SQL Server instance would be overkill and twice as expensive to run in Azure. MySQL or PostgreSQL would suffice, given that they have similair costs.
- *Service Bus* - Messaging queue for decoupling, message persistence and retry functionality. Messages might have to have short retention policy to minimize duplicate or outdated work.
- *Imbalance processor* - Determines which batteries need to re-evaluate their data and puts a message on the service bus
- *Battery processor* - Decides which command to send to the battery (if any) based on the pricing.

All running custom-build services (workers & cron job) are modeled as Azure Functions but could very well be container apps too. Both support timed executions & managed scaling based on events (KEDA in case of container apps) on the service bus.

## Other considerations

- I would maybe change the design to be run in an AKS as the backend scales up with more services, lower-latency is required or we want a more cloud-agnostic approach. In the latter case, I'd also move to RabbitMQ instead of Azure Service Bus.
- This design doesn't implement any observability components but proper alerting should be included to ensure that sending any failing commands are caught and resolved quickly. Additionally, Logging should be persisted somewhere with proper retention to allow for incident debugging.
- Maybe the algorithm outputs categories/classifications of batteries to determine the command to send. In that case, the components wouldn't change but the bulk of processing would be done in the *Imbalance processor*.
- instead of the *Service Bus*, *Azure Event Grid* would also be a valid option.