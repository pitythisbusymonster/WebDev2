
# ZAP Scanning Report
Generated on Sun, 14 Feb 2021 13:15:41

## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 2 |
| Medium | 3 |
| Low | 16 |
| Informational | 9 |

## Alerts

| Name | Risk Level | Number of Instances |
| --- | --- | --- | 
| Cross Site Scripting (Reflected) | High | 2 | 
| Remote OS Command Injection | High | 1 | 
| Cross-Domain Misconfiguration | Medium | 1 | 
| Format String Error | Medium | 1 | 
| X-Frame-Options Header Not Set | Medium | 34 | 
| Cookie Without Secure Flag | Low | 2 | 
| Incomplete or No Cache-control and Pragma HTTP Header Set | Low | 71 | 
| Server Leaks Information via "X-Powered-By" HTTP Response Header Field(s) | Low | 51 | 
| X-Content-Type-Options Header Missing | Low | 63 | 
| Charset Mismatch  | Informational | 1 | 
| Information Disclosure - Suspicious Comments | Informational | 3 | 
| Loosely Scoped Cookie | Informational | 6 | 
| Timestamp Disclosure - Unix | Informational | 261 | 






##Issues I Chose to Mitigate

| Remote OS Command Injection | High | 1 |
Research led me to believe that a lack of validation on my PageName property in my Reply View Model was the origin of this issue. I added a [StringLength()] attribute to PageName in my ReplyVm.cs file and the issue was relieved (along with 	some others, so they may have all been related. 
These others were: Format String Error, Cross-Domain Misconfiguration, Charset Mismatch, and Timestamp Disclosure.)


| X-Frame-Options Header Not Set | Medium | 34 |
To my Startup.cs file, in my Configure() method, I added an app.Use() statement setting a Header "X-Frame-Options" to "SAMEORIGIN".


| Cookie Without Secure Flag | Low | 2 |
To my Startup.cs file, in my Configure() method, I added an app.UseCookiePolicy() to secure cookie transactions.


| X-Content-Type-Options Header Missing | Low | 63 | 
To my Startup.cs file, in my Configure() method, I added an app.Use() statement setting a Header "X-Content-Type-Options" to "nosniff".	





# ZAP Scanning Report
Generated on Sun, 14 Feb 2021 17:25:31

## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 1 |
| Medium | 0 |
| Low | 2 |
| Informational | 3 |

## Alerts

| Name | Risk Level | Number of Instances |
| --- | --- | --- | 
| Cross Site Scripting (Reflected) | High | 2 | 
| Incomplete or No Cache-control and Pragma HTTP Header Set | Low | 1092 | 
| Server Leaks Information via "X-Powered-By" HTTP Response Header Field(s) | Low | 1635 | 
| Information Disclosure - Suspicious Comments | Informational | 4 | 
| Loosely Scoped Cookie | Informational | 2 | 



  
  
  
  
