# This tool uses swagger-codegen to generate classes from the EHS Insight OpenAPI / Swagger documentation in various languages.
# More information about swagger-codegen can be found at https://github.com/swagger-api/swagger-codegen

# Prerequisites:
# - Java 11+

# Available languages:
# - aspnetcore
# - csharp
# - csharp-dotnet2
# - go
# - go-server
# - dynamic-html
# - html
# - html2
# - java
# - jaxrs-cxf-client
# - jaxrs-cxf
# - inflector
# - jaxrs-cxf-cdi
# - jaxrs-spec
# - jaxrs-jersey
# - jaxrs-di
# - jaxrs-resteasy-eap
# - jaxrs-resteasy
# - java-vertx
# - micronaut
# - spring
# - nodejs-server
# - openapi
# - openapi-yaml
# - kotlin-client
# - kotlin-server
# - php
# - python
# - python-flask
# - r
# - ruby
# - scala
# - scala-akka-http-server
# - swift3
# - swift4
# - swift5
# - typescript-angular
# - typescript-axios
# - typescript-fetch
# - javascript


# ------------------------------------------------------------------------------------ #
# Configuration
# ------------------------------------------------------------------------------------ #

$openApiUrl = "https://YOURSITE.ehsinsight.com/api/v6/openapi";     # Replace with the target URL
$apiKey = "YOURAPIKEY";     # Replace with your API Key

$language = "csharp";
$outputPath = ".\Output";
$reservedword = "List=List"; # Mapping reserved words to their generated value 
$filter = ""; # Leave empty to generate all classes or include an entityname or reportname to only generate classes matching the filter keyword

# ------------------------------------------------------------------------------------ #


# Download pre-compiled binary
if (-Not (Test-Path -Path swagger-codegen-cli.jar)) {
    Write-Output "Downloading pre-compile swagger-codegen binary.";
    Invoke-WebRequest -OutFile swagger-codegen-cli.jar https://repo1.maven.org/maven2/io/swagger/codegen/v3/swagger-codegen-cli/3.0.57/swagger-codegen-cli-3.0.57.jar;
}

Write-Output "Fetching OpenAPI specification from "$openApiUrl"?filter="$filter;

# remove existing output folder
Remove-Item $outputPath -Force -Recurse -ErrorAction SilentlyContinue

 # Create models output folder
$outputModelsFolder = Join-Path -Path $outputPath -ChildPath "src\DotNetSamples\Models" 
      
if(!(Test-Path -Path $outputModelsFolder)) {
    New-Item -ItemType Directory -Path $outputModelsFolder
    Write-Host "Directory '$outputModelsFolder' created."
} else {
    Write-Host "Directory '$outputModelsFolder' already exists."
}

# Create supporting files output folder
$outputClientFolder = Join-Path -Path $outputPath -ChildPath "src\DotNetSamples\Client" 

if(!(Test-Path -Path $outputClientFolder)) {
    New-Item -ItemType Directory -Path $outputClientFolder
    Write-Host "Directory '$outputClientFolder' created."
} else {
    Write-Host "Directory '$outputClientFolder' already exists."
}

# Create supporting files project folder
$projectClientFolder = "";

if ($language -eq "csharp") {   
    $projectClientFolder = "..\DotNetSamples\Client" 
    if(!(Test-Path -Path $projectClientFolder)) {
        New-Item -ItemType Directory -Path $projectClientFolder
        Write-Host "Directory '$projectClientFolder' created."
    } else {
        Write-Host "Directory '$projectClientFolder' already exists."
    }
}
 
# Generate models
java -jar -Dmodels -DmodelDocs=false -DmaxYamlCodePoints=99999999 -DmodelTests=false -DsupportingFiles=true swagger-codegen-cli.jar generate -i $openApiUrl"?category=entity&filter="$filter -a "X-ApiKey: $apiKey" --reserved-words-mappings $reservedword -c config.json -l $language -o $outputPath;
java -jar -Dmodels -DmodelDocs=false -DmaxYamlCodePoints=99999999 -DmodelTests=false -DsupportingFiles=true swagger-codegen-cli.jar generate -i $openApiUrl"?category=hierarchy&filter="$filter -a "X-ApiKey: $apiKey" --reserved-words-mappings $reservedword -c config.json -l $language -o $outputPath;
java -jar -Dmodels -DmodelDocs=false -DmaxYamlCodePoints=99999999 -DmodelTests=false -DsupportingFiles=true swagger-codegen-cli.jar generate -i $openApiUrl"?category=role&filter="$filter -a "X-ApiKey: $apiKey" --reserved-words-mappings $reservedword -c config.json -l $language -o $outputPath;
java -jar -Dmodels -DmodelDocs=false -DmaxYamlCodePoints=99999999 -DmodelTests=false -DsupportingFiles=true swagger-codegen-cli.jar generate -i $openApiUrl"?category=report&filter="$filter -a "X-ApiKey: $apiKey" --reserved-words-mappings $reservedword -c config.json -l $language -o $outputPath;
java -jar -Dmodels -DmodelDocs=false -DmaxYamlCodePoints=99999999 -DmodelTests=false -DsupportingFiles=true swagger-codegen-cli.jar generate -i $openApiUrl"?category=attachment&filter="$filter -a "X-ApiKey: $apiKey" --reserved-words-mappings $reservedword -c config.json -l $language -o $outputPath;
java -jar -Dmodels -DmodelDocs=false -DmaxYamlCodePoints=99999999 -DmodelTests=false -DsupportingFiles=true swagger-codegen-cli.jar generate -i $openApiUrl"?category=sdscommunity&filter="$filter -a "X-ApiKey: $apiKey" --reserved-words-mappings $reservedword -c config.json -l $language -o $outputPath;

if ($language -eq "csharp") {
    # Copy classes into the DotNetSamples project
    Copy-Item -Path $outputModelsFolder"\*.cs" -Destination "..\DotNetSamples\Models" -Force
    Copy-Item -Path $outputClientFolder"\SwaggerDateConverter.cs" -Destination "..\DotNetSamples\Client" -Force
    Remove-Item $outputPath -Force -Recurse -ErrorAction SilentlyContinue
} else {
    # Open the output folder
    Start-Process -FilePath $outputPath
}
