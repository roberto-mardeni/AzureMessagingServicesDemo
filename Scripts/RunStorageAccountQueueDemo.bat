pushd .

cd "StorageAccountQueueReceiver\bin\Debug\netcoreapp3.1"

start cmd.exe /k "dotnet StorageAccountQueueReceiver.dll"
start cmd.exe /k "dotnet StorageAccountQueueReceiver.dll"

popd

pushd .

cd "StorageAccountQueueSender\bin\Debug\netcoreapp3.1"

start cmd.exe /k "dotnet StorageAccountQueueSender.dll"

popd