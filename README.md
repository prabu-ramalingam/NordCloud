NordCloud!
High Level Architecture
[arch](https://user-images.githubusercontent.com/60443784/167133973-0500ad69-6ee8-4faa-966a-607f18f9aff5.png)

Partially Implemented:
1.	Web UI
2.	Gateway Service 
    a.	Auth/Auth – No tests
    b.	Configuration – No configuration for environments and missed to include some key value as well 
    c.	No Integration Tests
    d.	Centralized Logs/ Health Monitor are not tested. But included Serilog  with elastic stack 
    e.	No Tests with downstream services
3.	Services – 
    a.	No endpoint for update 
    b.	Unit Tests and Integration Tests covered for Get Events endpoint.  
4.	No Caching

To be improved:
  •	Automate the Deployment of Microservices 
  •	Manage Microservices Configuration
  •	Scalable Messaging Patterns including Fail Over 
  •	Scalable, on-demand databases
  •	Favor event sourcing and CQRS pattern
  •	Use intelligent caching to improve resilience
  •	Test in production-like environments
  •	CI jobs triggered on code check-in
  •	Microservices Monitoring
  •	Externalized Configuration Pattern
  •	Not much test cases

