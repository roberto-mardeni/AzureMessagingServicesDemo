# AzureMessagingServicesDemo
A demo on some of Azure Messaging Services including:

- ARM Templates to deploy the services used
- Azure Storage Queues
- Service Bus Queues & Topics

## How to use

1. Deploy the ARM Templated provided in the Environment project to your desired Azure region.
2. Use the output values in the template to create an ***appsettings.json*** file in the root of the repository, it should look like

    > ```json
    > {
    >   "storageAccountConnectionString": "DefaultEndpointsProtocol=https;AccountName=<name>;AccountKey=<key>;EndpointSuffix=core.windows.net",
    >   "serviceBusNamespaceConnectionString": "Endpoint=sb://<name>.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=<key>"
    > }
    > ```

3. Build the solution in Visual Studio or VS Code
4. Use the bat files in the ***Scripts*** directory to run each of the demos
