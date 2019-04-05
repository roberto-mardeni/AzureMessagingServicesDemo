pushd .

cd "ServiceBusQueueMessageReceiver\bin\Debug\netcoreapp2.2"

start cmd.exe /k "dotnet ServiceBusQueueMessageReceiver.dll"

popd

pushd .

cd "ServiceBusQueueMessageSender\bin\Debug\netcoreapp2.2"

start cmd.exe /k "dotnet ServiceBusQueueMessageSender.dll"

popd