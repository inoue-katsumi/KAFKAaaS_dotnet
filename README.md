# KAFKAaaS_dotnet

KAFKAaaS_dotnet is dotnet implementation sample of Kafka producer client. 

This client published Youtube search result to Kafka topic as JSON.

Tested on Windows10 WSL2(Windows Subsystem for Linux ver.2) env.

## Usage:

```bash
dotnet.exe build producer_stdin.csproj
export topic_name=youtube_json_x
python3 youtube_list.py |
sed  "s/ '/ \"/g;s/{'/{\"/g;s/'}/\"}/g;s/':/\":/g;s/',/\",/g" |
bin/Debug/netcoreapp6.0/producer_youtube_search.exe $(wslpath -w getting-started.properties) \
  1 ${topic_name}
```

## getting-started.properties:

Edit this file as necessary. Originally from Confluent.

## producer_youtube_search.cs:

C# source code for Kafka sample producer.

Reads 2 lines from STDIN and publishes this pair as 1 Kafka message.

1st line: Key

2nd line: Value

## producer_youtube_search.csproj:

C# project file.

## youtube_list.py:

Searchs youtube with simple keyword and find just 1 recent video.

Write out 2 JSON lines. 

1st line: videoid

2nd line: description
