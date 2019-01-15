.PHONY: clean package deploy
.DEFAULT_GOAL := $(PROD_BIN_PATH)

REGION = us-east-1
PROJ_NAME = DotNetCoreLambdaPlayground
SRC_DIR = src/$(PROJ_NAME)
FN_FILE = $(SRC_DIR)/Function.cs

PROD_BIN_PATH = $(SRC_DIR)/bin/Release/netcoreapp2.1/publish/$(PROJ_NAME).dll
SAM_CFN_FILE = template.yaml
REAL_CFN_FILE = $(S3_BUCKET_NAME).yaml

# NOTE: This bucket must:
# - be created manually before running `package` or `deploy`, and
# - reside in `REGION`
S3_BUCKET_NAME = dotnet-core-lambda-playground

$(PROD_BIN_PATH): $(FN_FILE)
	dotnet publish -c Release

clean:
	rm $(PROD_BIN_PATH)

package: $(SAM_CFN_FILE) $(PROD_BIN_PATH)
	sam package \
		--template-file $(SAM_CFN_FILE) \
		--output-template $(REAL_CFN_FILE) \
		--s3-bucket $(S3_BUCKET_NAME)

deploy: package
	sam deploy \
		--template-file $(REAL_CFN_FILE) \
		--stack-name $(PROJ_NAME) \
		--capabilities CAPABILITY_IAM \
		--region $(REGION)