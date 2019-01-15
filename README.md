# `DotNetCoreLambdaPlayground`

### Building

```
$ make
```

### Packaging via [aws-sam-cli](https://github.com/awslabs/aws-sam-cli)

```
$ make package
```

### Deploying via [aws-sam-cli](https://github.com/awslabs/aws-sam-cli)

```
$ make deploy
```

⚠️ Before deploying you *must* create the bucket that is specified in `Makefile` ⚠️

The bucket name is `dotnet-core-lambda-playground` so you might create that bucket like so:

```
$ aws mb s3://dotnet-core-lambda-playground
```

⚠️ You *must* pick a different bucket name as this one already exists ⚠️