pushd .

cd "ServiceBusTopicSubscriber\bin\Debug\netcoreapp3.1"

start cmd.exe /k "dotnet ServiceBusTopicSubscriber.dll subscription1"
start cmd.exe /k "dotnet ServiceBusTopicSubscriber.dll subscription2"

popd

pushd .

cd "ServiceBusTopicSender\bin\Debug\netcoreapp3.1"

start cmd.exe /k "dotnet ServiceBusTopicSender.dll"

popd