
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

## Alert Detail


  
  
  
  
### Cross Site Scripting (Reflected)
##### High (Low)
  
  
  
  
#### Description
<p>Cross-site Scripting (XSS) is an attack technique that involves echoing attacker-supplied code into a user's browser instance. A browser instance can be a standard web browser client, or a browser object embedded in a software product such as the browser within WinAmp, an RSS reader, or an email client. The code itself is usually written in HTML/JavaScript, but may also extend to VBScript, ActiveX, Java, Flash, or any other browser-supported technology.</p><p>When an attacker gets a user's browser to execute his/her code, the code will run within the security context (or zone) of the hosting web site. With this level of privilege, the code has the ability to read, modify and transmit any sensitive data accessible by the browser. A Cross-site Scripted user could have his/her account hijacked (cookie theft), their browser redirected to another location, or possibly shown fraudulent content delivered by the web site they are visiting. Cross-site Scripting attacks essentially compromise the trust relationship between a user and the web site. Applications utilizing browser object instances which load content from the file system may execute code under the local machine zone allowing for system compromise.</p><p></p><p>There are three types of Cross-site Scripting attacks: non-persistent, persistent and DOM-based.</p><p>Non-persistent attacks and DOM-based attacks require a user to either visit a specially crafted link laced with malicious code, or visit a malicious web page containing a web form, which when posted to the vulnerable site, will mount the attack. Using a malicious form will oftentimes take place when the vulnerable resource only accepts HTTP POST requests. In such a case, the form can be submitted automatically, without the victim's knowledge (e.g. by using JavaScript). Upon clicking on the malicious link or submitting the malicious form, the XSS payload will get echoed back and will get interpreted by the user's browser and execute. Another technique to send almost arbitrary requests (GET and POST) is by using an embedded client, such as Adobe Flash.</p><p>Persistent attacks occur when the malicious code is submitted to a web site where it's stored for a period of time. Examples of an attacker's favorite targets often include message board posts, web mail messages, and web chat software. The unsuspecting user is not required to interact with any additional site/link (e.g. an attacker site or a malicious link sent via email), just simply view the web page containing the code.</p>
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D887](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D887)
  
  
  * Method: `POST`
  
  
  * Parameter: `RememberMe`
  
  
  * Attack: `<script>alert(1);</script>`
  
  
  * Evidence: `<script>alert(1);</script>`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn](https://localhost:44372/Account/LogIn)
  
  
  * Method: `POST`
  
  
  * Parameter: `RememberMe`
  
  
  * Attack: `<script>alert(1);</script>`
  
  
  * Evidence: `<script>alert(1);</script>`
  
  
  
  
Instances: 2
  
### Solution
<p>Phase: Architecture and Design</p><p>Use a vetted library or framework that does not allow this weakness to occur or provides constructs that make this weakness easier to avoid.</p><p>Examples of libraries and frameworks that make it easier to generate properly encoded output include Microsoft's Anti-XSS library, the OWASP ESAPI Encoding module, and Apache Wicket.</p><p></p><p>Phases: Implementation; Architecture and Design</p><p>Understand the context in which your data will be used and the encoding that will be expected. This is especially important when transmitting data between different components, or when generating outputs that can contain multiple encodings at the same time, such as web pages or multi-part mail messages. Study all expected communication protocols and data representations to determine the required encoding strategies.</p><p>For any data that will be output to another web page, especially any data that was received from external inputs, use the appropriate encoding on all non-alphanumeric characters.</p><p>Consult the XSS Prevention Cheat Sheet for more details on the types of encoding and escaping that are needed.</p><p></p><p>Phase: Architecture and Design</p><p>For any security checks that are performed on the client side, ensure that these checks are duplicated on the server side, in order to avoid CWE-602. Attackers can bypass the client-side checks by modifying values after the checks have been performed, or by changing the client to remove the client-side checks entirely. Then, these modified values would be submitted to the server.</p><p></p><p>If available, use structured mechanisms that automatically enforce the separation between data and code. These mechanisms may be able to provide the relevant quoting, encoding, and validation automatically, instead of relying on the developer to provide this capability at every point where output is generated.</p><p></p><p>Phase: Implementation</p><p>For every web page that is generated, use and specify a character encoding such as ISO-8859-1 or UTF-8. When an encoding is not specified, the web browser may choose a different encoding by guessing which encoding is actually being used by the web page. This can cause the web browser to treat certain sequences as special, opening up the client to subtle XSS attacks. See CWE-116 for more mitigations related to encoding/escaping.</p><p></p><p>To help mitigate XSS attacks against the user's session cookie, set the session cookie to be HttpOnly. In browsers that support the HttpOnly feature (such as more recent versions of Internet Explorer and Firefox), this attribute can prevent the user's session cookie from being accessible to malicious client-side scripts that use document.cookie. This is not a complete solution, since HttpOnly is not supported by all browsers. More importantly, XMLHTTPRequest and other powerful browser technologies provide read access to HTTP headers, including the Set-Cookie header in which the HttpOnly flag is set.</p><p></p><p>Assume all input is malicious. Use an "accept known good" input validation strategy, i.e., use an allow list of acceptable inputs that strictly conform to specifications. Reject any input that does not strictly conform to specifications, or transform it into something that does. Do not rely exclusively on looking for malicious or malformed inputs (i.e., do not rely on a deny list). However, deny lists can be useful for detecting potential attacks or determining which inputs are so malformed that they should be rejected outright.</p><p></p><p>When performing input validation, consider all potentially relevant properties, including length, type of input, the full range of acceptable values, missing or extra inputs, syntax, consistency across related fields, and conformance to business rules. As an example of business rule logic, "boat" may be syntactically valid because it only contains alphanumeric characters, but it is not valid if you are expecting colors such as "red" or "blue."</p><p></p><p>Ensure that you perform input validation at well-defined interfaces within the application. This will help protect the application even if a component is reused or moved elsewhere.</p>
  
### Other information
<p>Raised with LOW confidence as the Content-Type is not HTML</p>
  
### Reference
* http://projects.webappsec.org/Cross-Site-Scripting
* http://cwe.mitre.org/data/definitions/79.html

  
#### CWE Id : 79
  
#### WASC Id : 8
  
#### Source ID : 1

  
  
  
  
### Incomplete or No Cache-control and Pragma HTTP Header Set
##### Low (Medium)
  
  
  
  
#### Description
<p>The cache-control and pragma HTTP header have not been set properly or are missing allowing the browser and proxies to cache content.</p>
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D9](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D9)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D621](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D621)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D173](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D173)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D391](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D391)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D887](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D887)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D536](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D536)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D618](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D618)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D703](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D703)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D620](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D620)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D172](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D172)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D390](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D390)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FForum](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FForum)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D886](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D886)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D535](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D535)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D617](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D617)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D702](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D702)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D835](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D835)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D401](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D401)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D885](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D885)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D534](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D534)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
Instances: 1092
  
### Solution
<p>Whenever possible ensure the cache-control HTTP header is set with no-cache, no-store, must-revalidate; and that the pragma HTTP header is set with no-cache.</p>
  
### Reference
* https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching

  
#### CWE Id : 525
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Server Leaks Information via "X-Powered-By" HTTP Response Header Field(s)
##### Low (Medium)
  
  
  
  
#### Description
<p>The web/application server is leaking information via one or more "X-Powered-By" HTTP response headers. Access to such information may facilitate attackers identifying other frameworks/components your web application is reliant upon and the vulnerabilities such components may be subject to.</p>
  
  
  
* URL: [https://localhost:44372/Home/Reply?forumId=373](https://localhost:44372/Home/Reply?forumId=373)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Home/Reply?forumId=240](https://localhost:44372/Home/Reply?forumId=240)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D466](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D466)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Home/Reply?forumId=288](https://localhost:44372/Home/Reply?forumId=288)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D64](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D64)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D248](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D248)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D333](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D333)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D599](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D599)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Home/Reply?forumId=155](https://localhost:44372/Home/Reply?forumId=155)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D551](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D551)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D684](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D684)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D250](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D250)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D383](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D383)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D165](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D165)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Home/Reply?forumId=818](https://localhost:44372/Home/Reply?forumId=818)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D298](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D298)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Home/Reply?forumId=372](https://localhost:44372/Home/Reply?forumId=372)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D249](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D249)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D63](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D63)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D685](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D685)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
Instances: 1635
  
### Solution
<p>Ensure that your web server, application server, load balancer, etc. is configured to suppress "X-Powered-By" headers.</p>
  
### Reference
* http://blogs.msdn.com/b/varunm/archive/2013/04/23/remove-unwanted-http-response-headers.aspx
* http://www.troyhunt.com/2012/02/shhh-dont-let-your-response-headers.html

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Information Disclosure - Suspicious Comments
##### Informational (Medium)
  
  
  
  
#### Description
<p>The response appears to contain suspicious comments which may help an attacker. Note: Matches made within script blocks or files are against the entire content not only comments.</p>
  
  
  
* URL: [https://localhost:44372](https://localhost:44372)
  
  
  * Method: `GET`
  
  
  * Evidence: `Admin`
  
  
  
  
* URL: [https://localhost:44372/](https://localhost:44372/)
  
  
  * Method: `GET`
  
  
  * Evidence: `Admin`
  
  
  
  
Instances: 2
  
### Solution
<p>Remove all comments that return information that may help an attacker and fix any underlying problems they refer to.</p>
  
### Other information
<p>The following pattern was used: \bADMIN\b and was detected in the element starting with: "<!--<div><a href="./Admin/Index"> Admin</a></div>-->", see evidence field for the suspicious comment/snippet.</p>
  
### Reference
* 

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Information Disclosure - Suspicious Comments
##### Informational (Low)
  
  
  
  
#### Description
<p>The response appears to contain suspicious comments which may help an attacker. Note: Matches made within script blocks or files are against the entire content not only comments.</p>
  
  
  
* URL: [https://localhost:44372/lib/bootstrap/dist/js/bootstrap.bundle.min.js](https://localhost:44372/lib/bootstrap/dist/js/bootstrap.bundle.min.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `from`
  
  
  
  
* URL: [https://localhost:44372/lib/jquery/dist/jquery.min.js](https://localhost:44372/lib/jquery/dist/jquery.min.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `username`
  
  
  
  
Instances: 2
  
### Solution
<p>Remove all comments that return information that may help an attacker and fix any underlying problems they refer to.</p>
  
### Other information
<p>The following pattern was used: \bFROM\b and was detected in the element starting with: "!function(t,e){"object"==typeof exports&&"undefined"!=typeof module?e(exports,require("jquery")):"function"==typeof define&&defi", see evidence field for the suspicious comment/snippet.</p>
  
### Reference
* 

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Loosely Scoped Cookie
##### Informational (Low)
  
  
  
  
#### Description
<p>Cookies can be scoped by domain or path. This check is only concerned with domain scope.The domain scope applied to a cookie determines which domains can access it. For example, a cookie can be scoped strictly to a subdomain e.g. www.nottrusted.com, or loosely scoped to a parent domain e.g. nottrusted.com. In the latter case, any subdomain of nottrusted.com can access the cookie. Loosely scoped cookies are common in mega-applications like google.com and live.com. Cookies set from a subdomain like app.foo.bar are transmitted only to that domain by the browser. However, cookies scoped to a parent-level domain may be transmitted to the parent, or any subdomain of the parent.</p>
  
  
  
* URL: [https://localhost:44372/Account/Register](https://localhost:44372/Account/Register)
  
  
  * Method: `GET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login](https://localhost:44372/Account/Login)
  
  
  * Method: `GET`
  
  
  
  
Instances: 2
  
### Solution
<p>Always scope cookies to a FQDN (Fully Qualified Domain Name).</p>
  
### Other information
<p>The origin domain used for comparison was: </p><p>localhost</p><p>.AspNetCore.Antiforgery.A4ZnHEKLWuk=CfDJ8J25h_GVVAlAudqFyu1vM_N3VMvSvl0FEbN3vdDBxt1JMD_7LhszD7UEt5m2a_Qik7w8LjCjh5QA6lzVQoVucll4RpVDCPnTepiGhRCfrf1KDG8yktT1CWlxxodhjOFmRH5TjW0izif4UsGLOEwnZHY</p><p></p>
  
### Reference
* https://tools.ietf.org/html/rfc6265#section-4.1
* https://owasp.org/www-project-web-security-testing-guide/v41/4-Web_Application_Security_Testing/06-Session_Management_Testing/02-Testing_for_Cookies_Attributes.html
* http://code.google.com/p/browsersec/wiki/Part2#Same-origin_policy_for_cookies

  
#### CWE Id : 565
  
#### WASC Id : 15
  
#### Source ID : 3
