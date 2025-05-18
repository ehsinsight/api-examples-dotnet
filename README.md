# Summary
This C# example project demonstrates the following API actions:
* Add, Update, and Delete a user
* Add, Update, and Delete a form
* Add, Update, and Delete a business hierarchy
* Add and Delete an employer
* Add and Delete a position
* Add an attachment
* Execute a report

Full API Documentation is located at **Help / API Documentation**.

The examples can be adapted to cover additional entity types by referencing **Help / Schema Explorer** to discover additional entity names and property types.


# Caution
DO NOT RUN API EXAMPLES AGAINST A PRODUCTION SITE.

Please request a **SANDBOX** site for API experimentation and development.

# Instructions

#### Running Examples
1. Adjust variables in the `Settings.cs`
    * Update `apiKey` with an API key generated in EHS Insight under **Administration / API Settings**
    * Update `siteUrl` with your EHS Insight base URL (ex. `https://yoursite.ehsinsight.com`)
1. Adapt these examples to your use cases

#### Generating Additional Classes
1. Install Java 8+
1. Adjust the variables in the Configuration section of `CodeGenerator\GenerateEhsInsightModels.ps1`
    * Update the `apiKey` variable with an API key generated in EHS Insight under **Administration / API Settings**
    * Update the `openApiUrl` variable with your EHS Insight site name    
1. Run `CodeGenerator\GenerateEhsInsightModels.ps1` using PowerShell
1. Additional classes should now be added to the DotNetSamples project covering the entire API (with your site settings, user-defined fields, and user-defined report queries included)

# License
This repository contains programming examples.

EHS Insight grants you a nonexclusive copyright license to use all programming code examples from which you can generate similar function tailored to your own specific needs.

All sample code is provided by EHS Insight for illustrative purposes only. These examples have not been thoroughly tested under all conditions. 

EHS Insight, therefore, cannot guarantee or imply reliability, serviceability, or function of these programs.

All programs contained herein are provided to you "AS IS" without any warranties of any kind. 

The implied warranties of non-infringement, merchantability and fitness for a particular purpose are expressly disclaimed.
