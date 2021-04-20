pushd .

cd "ServiceBusQueueMessageReceiver\bin\Debug\netcoreapp3.1"

start cmd.exe /k "dotnet ServiceBusQueueMessageReceiver.dll"

popd

pushd .

cd "ServiceBusQueueMessageSender\bin\Debug\netcoreapp3.1"

start cmd.exe /k "dotnet ServiceBusQueueMessageSender.dll"

popd