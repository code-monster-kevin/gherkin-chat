# Simple Webchat Application using Azure SignalR and RESTful API

## Setup
### Pre-requisites
1. Azure SignalR account
2. MSSQL Server

### Steps to run this locally
1. Clone the solution via git clone to your local directory
2. Change the configuration in appsettings.json
```
  "ConnectionStrings": {
    "GherkinChatDB": "<your sql connection string>"
  }
  ...
  "Azure": {
    "SignalR": {
      "ConnectionString": "<your azure signalr connection"
    }
```

