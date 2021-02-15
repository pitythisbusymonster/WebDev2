
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

## Alert Detail


  
  
  
  
### Cross Site Scripting (Reflected)
##### High (Low)
  
  
  
  
#### Description
<p>Cross-site Scripting (XSS) is an attack technique that involves echoing attacker-supplied code into a user's browser instance. A browser instance can be a standard web browser client, or a browser object embedded in a software product such as the browser within WinAmp, an RSS reader, or an email client. The code itself is usually written in HTML/JavaScript, but may also extend to VBScript, ActiveX, Java, Flash, or any other browser-supported technology.</p><p>When an attacker gets a user's browser to execute his/her code, the code will run within the security context (or zone) of the hosting web site. With this level of privilege, the code has the ability to read, modify and transmit any sensitive data accessible by the browser. A Cross-site Scripted user could have his/her account hijacked (cookie theft), their browser redirected to another location, or possibly shown fraudulent content delivered by the web site they are visiting. Cross-site Scripting attacks essentially compromise the trust relationship between a user and the web site. Applications utilizing browser object instances which load content from the file system may execute code under the local machine zone allowing for system compromise.</p><p></p><p>There are three types of Cross-site Scripting attacks: non-persistent, persistent and DOM-based.</p><p>Non-persistent attacks and DOM-based attacks require a user to either visit a specially crafted link laced with malicious code, or visit a malicious web page containing a web form, which when posted to the vulnerable site, will mount the attack. Using a malicious form will oftentimes take place when the vulnerable resource only accepts HTTP POST requests. In such a case, the form can be submitted automatically, without the victim's knowledge (e.g. by using JavaScript). Upon clicking on the malicious link or submitting the malicious form, the XSS payload will get echoed back and will get interpreted by the user's browser and execute. Another technique to send almost arbitrary requests (GET and POST) is by using an embedded client, such as Adobe Flash.</p><p>Persistent attacks occur when the malicious code is submitted to a web site where it's stored for a period of time. Examples of an attacker's favorite targets often include message board posts, web mail messages, and web chat software. The unsuspecting user is not required to interact with any additional site/link (e.g. an attacker site or a malicious link sent via email), just simply view the web page containing the code.</p>
  
  
  
* URL: [https://localhost:44372/Account/LogIn](https://localhost:44372/Account/LogIn)
  
  
  * Method: `POST`
  
  
  * Parameter: `RememberMe`
  
  
  * Attack: `<script>alert(1);</script>`
  
  
  * Evidence: `<script>alert(1);</script>`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FAdmin%2FIndex](https://localhost:44372/Account/LogIn?returnUrl=%2FAdmin%2FIndex)
  
  
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

  
  
  
  
### Remote OS Command Injection
##### High (Medium)
  
  
  
  
#### Description
<p>Attack technique used for unauthorized execution of operating system commands. This attack is possible when an application accepts untrusted input to build operating system commands in an insecure manner involving improper data sanitization, and/or improper calling of external programs.</p>
  
  
  
* URL: [https://localhost:44372/Home/Forum](https://localhost:44372/Home/Forum)
  
  
  * Method: `POST`
  
  
  * Parameter: `PageName`
  
  
  * Attack: `ZapTest;start-sleep -s 15 #`
  
  
  
  
Instances: 1
  
### Solution
<p>If at all possible, use library calls rather than external processes to recreate the desired functionality.</p><p></p><p>Run your code in a "jail" or similar sandbox environment that enforces strict boundaries between the process and the operating system. This may effectively restrict which files can be accessed in a particular directory or which commands can be executed by your software.</p><p></p><p>OS-level examples include the Unix chroot jail, AppArmor, and SELinux. In general, managed code may provide some protection. For example, java.io.FilePermission in the Java SecurityManager allows you to specify restrictions on file operations.</p><p>This may not be a feasible solution, and it only limits the impact to the operating system; the rest of your application may still be subject to compromise.</p><p></p><p>For any data that will be used to generate a command to be executed, keep as much of that data out of external control as possible. For example, in web applications, this may require storing the command locally in the session's state instead of sending it out to the client in a hidden form field.</p><p></p><p>Use a vetted library or framework that does not allow this weakness to occur or provides constructs that make this weakness easier to avoid.</p><p></p><p>For example, consider using the ESAPI Encoding control or a similar tool, library, or framework. These will help the programmer encode outputs in a manner less prone to error.</p><p></p><p>If you need to use dynamically-generated query strings or commands in spite of the risk, properly quote arguments and escape any special characters within those arguments. The most conservative approach is to escape or filter all characters that do not pass an extremely strict allow list (such as everything that is not alphanumeric or white space). If some special characters are still needed, such as white space, wrap each argument in quotes after the escaping/filtering step. Be careful of argument injection.</p><p></p><p>If the program to be executed allows arguments to be specified within an input file or from standard input, then consider using that mode to pass arguments instead of the command line.</p><p></p><p>If available, use structured mechanisms that automatically enforce the separation between data and code. These mechanisms may be able to provide the relevant quoting, encoding, and validation automatically, instead of relying on the developer to provide this capability at every point where output is generated.</p><p></p><p>Some languages offer multiple functions that can be used to invoke commands. Where possible, identify any function that invokes a command shell using a single string, and replace it with a function that requires individual arguments. These functions typically perform appropriate quoting and filtering of arguments. For example, in C, the system() function accepts a string that contains the entire command to be executed, whereas execl(), execve(), and others require an array of strings, one for each argument. In Windows, CreateProcess() only accepts one command at a time. In Perl, if system() is provided with an array of arguments, then it will quote each of the arguments.</p><p></p><p>Assume all input is malicious. Use an "accept known good" input validation strategy, i.e., use an allow list of acceptable inputs that strictly conform to specifications. Reject any input that does not strictly conform to specifications, or transform it into something that does. Do not rely exclusively on looking for malicious or malformed inputs (i.e., do not rely on a deny list). However, deny lists can be useful for detecting potential attacks or determining which inputs are so malformed that they should be rejected outright.</p><p></p><p>When performing input validation, consider all potentially relevant properties, including length, type of input, the full range of acceptable values, missing or extra inputs, syntax, consistency across related fields, and conformance to business rules. As an example of business rule logic, "boat" may be syntactically valid because it only contains alphanumeric characters, but it is not valid if you are expecting colors such as "red" or "blue."</p><p></p><p>When constructing OS command strings, use stringent allow lists that limit the character set based on the expected value of the parameter in the request. This will indirectly limit the scope of an attack, but this technique is less important than proper output encoding and escaping.</p><p></p><p>Note that proper output encoding, escaping, and quoting is the most effective solution for preventing OS command injection, although input validation may provide some defense-in-depth. This is because it effectively limits what will appear in output. Input validation will not always prevent OS command injection, especially if you are required to support free-form text fields that could contain arbitrary characters. For example, when invoking a mail program, you might need to allow the subject field to contain otherwise-dangerous inputs like ";" and ">" characters, which would need to be escaped or otherwise handled. In this case, stripping the character might reduce the risk of OS command injection, but it would produce incorrect behavior because the subject field would not be recorded as the user intended. This might seem to be a minor inconvenience, but it could be more important when the program relies on well-structured subject lines in order to pass messages to other components.</p><p></p><p>Even if you make a mistake in your validation (such as forgetting one out of 100 input fields), appropriate encoding is still likely to protect you from injection-based attacks. As long as it is not done in isolation, input validation is still a useful technique, since it may significantly reduce your attack surface, allow you to detect some attacks, and provide other security benefits that proper encoding does not address.</p>
  
### Reference
* http://cwe.mitre.org/data/definitions/78.html
* https://owasp.org/www-community/attacks/Command_Injection

  
#### CWE Id : 78
  
#### WASC Id : 31
  
#### Source ID : 1

  
  
  
  
### Cross-Domain Misconfiguration
##### Medium (Medium)
  
  
  
  
#### Description
<p>Web browser data loading may be possible, due to a Cross Origin Resource Sharing (CORS) misconfiguration on the web server</p>
  
  
  
* URL: [https://location.services.mozilla.com/v1/country?key=7e40f68c-7938-4c5d-9f95-e61647c213eb](https://location.services.mozilla.com/v1/country?key=7e40f68c-7938-4c5d-9f95-e61647c213eb)
  
  
  * Method: `GET`
  
  
  * Evidence: `Access-Control-Allow-Origin: *`
  
  
  
  
Instances: 1
  
### Solution
<p>Ensure that sensitive data is not available in an unauthenticated manner (using IP address white-listing, for instance).</p><p>Configure the "Access-Control-Allow-Origin" HTTP header to a more restrictive set of domains, or remove all CORS headers entirely, to allow the web browser to enforce the Same Origin Policy (SOP) in a more restrictive manner.</p>
  
### Other information
<p>The CORS misconfiguration on the web server permits cross-domain read requests from arbitrary third party domains, using unauthenticated APIs on this domain. Web browser implementations do not permit arbitrary third parties to read the response from authenticated APIs, however. This reduces the risk somewhat. This misconfiguration could be used by an attacker to access data that is available in an unauthenticated manner, but which uses some other form of security, such as IP address white-listing.</p>
  
### Reference
* http://www.hpenterprisesecurity.com/vulncat/en/vulncat/vb/html5_overly_permissive_cors_policy.html

  
#### CWE Id : 264
  
#### WASC Id : 14
  
#### Source ID : 3

  
  
  
  
### Format String Error
##### Medium (Medium)
  
  
  
  
#### Description
<p>A Format String error occurs when the submitted data of an input string is evaluated as a command by the application. </p>
  
  
  
* URL: [https://localhost:44372/Home/Forum](https://localhost:44372/Home/Forum)
  
  
  * Method: `POST`
  
  
  * Parameter: `PageName`
  
  
  * Attack: `ZAP%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s
`
  
  
  
  
Instances: 1
  
### Solution
<p>Rewrite the background program using proper deletion of bad character strings.  This will require a recompile of the background executable.</p>
  
### Other information
<p>Potential Format String Error.  The script closed the connection on a /%s</p>
  
### Reference
* https://owasp.org/www-community/attacks/Format_string_attack

  
#### CWE Id : 134
  
#### WASC Id : 6
  
#### Source ID : 1

  
  
  
  
### X-Frame-Options Header Not Set
##### Medium (Medium)
  
  
  
  
#### Description
<p>X-Frame-Options header is not included in the HTTP response to protect against 'ClickJacking' attacks.</p>
  
  
  
* URL: [https://localhost:44372/Home/Reply?forumId=6](https://localhost:44372/Home/Reply?forumId=6)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn](https://localhost:44372/Account/LogIn)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D3](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D3)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D3](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D3)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/Home/Overview](https://localhost:44372/Home/Overview)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn](https://localhost:44372/Account/LogIn)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/TimeLine](https://localhost:44372/TimeLine)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D4](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D4)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D2](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D2)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/References/OnlineMedia](https://localhost:44372/References/OnlineMedia)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/References](https://localhost:44372/References)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D5](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D5)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D1](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D1)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/Login](https://localhost:44372/Account/Login)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/Admin](https://localhost:44372/Admin)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/](https://localhost:44372/)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D6](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D6)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/TimeLine/Quiz](https://localhost:44372/TimeLine/Quiz)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FAdmin%2FIndex](https://localhost:44372/Account/LogIn?returnUrl=%2FAdmin%2FIndex)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [https://localhost:44372/References/PrintMedia](https://localhost:44372/References/PrintMedia)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
Instances: 34
  
### Solution
<p>Most modern Web browsers support the X-Frame-Options HTTP header. Ensure it's set on all web pages returned by your site (if you expect the page to be framed only by pages on your server (e.g. it's part of a FRAMESET) then you'll want to use SAMEORIGIN, otherwise if you never expect the page to be framed, you should use DENY. Alternatively consider implementing Content Security Policy's "frame-ancestors" directive. </p>
  
### Reference
* https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options

  
#### CWE Id : 16
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
  
### Cookie Without Secure Flag
##### Low (Medium)
  
  
  
  
#### Description
<p>A cookie has been set without the secure flag, which means that the cookie can be accessed via unencrypted connections.</p>
  
  
  
* URL: [https://localhost:44372/Account/Login](https://localhost:44372/Account/Login)
  
  
  * Method: `GET`
  
  
  * Parameter: `.AspNetCore.Antiforgery.A4ZnHEKLWuk`
  
  
  * Evidence: `Set-Cookie: .AspNetCore.Antiforgery.A4ZnHEKLWuk`
  
  
  
  
* URL: [https://localhost:44372/Account/Register](https://localhost:44372/Account/Register)
  
  
  * Method: `GET`
  
  
  * Parameter: `.AspNetCore.Antiforgery.A4ZnHEKLWuk`
  
  
  * Evidence: `Set-Cookie: .AspNetCore.Antiforgery.A4ZnHEKLWuk`
  
  
  
  
Instances: 2
  
### Solution
<p>Whenever a cookie contains sensitive information or is a session token, then it should always be passed using an encrypted channel. Ensure that the secure flag is set for cookies containing such sensitive information.</p>
  
### Reference
* https://owasp.org/www-project-web-security-testing-guide/v41/4-Web_Application_Security_Testing/06-Session_Management_Testing/02-Testing_for_Cookies_Attributes.html

  
#### CWE Id : 614
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Incomplete or No Cache-control and Pragma HTTP Header Set
##### Low (Medium)
  
  
  
  
#### Description
<p>The cache-control and pragma HTTP header have not been set properly or are missing allowing the browser and proxies to cache content.</p>
  
  
  
* URL: [https://aus5.mozilla.org/update/3/SystemAddons/85.0.2/20210208133944/WINNT_x86_64-msvc-x64/en-US/release/Windows_NT%2010.0.0.0.19041.804%20(x64)/default/default/update.xml](https://aus5.mozilla.org/update/3/SystemAddons/85.0.2/20210208133944/WINNT_x86_64-msvc-x64/en-US/release/Windows_NT%2010.0.0.0.19041.804%20(x64)/default/default/update.xml)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `public, max-age=90`
  
  
  
  
Instances: 1
  
### Solution
<p>Whenever possible ensure the cache-control HTTP header is set with no-cache, no-store, must-revalidate; and that the pragma HTTP header is set with no-cache.</p>
  
### Reference
* https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching

  
#### CWE Id : 525
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Incomplete or No Cache-control and Pragma HTTP Header Set
##### Low (Medium)
  
  
  
  
#### Description
<p>The cache-control and pragma HTTP header have not been set properly or are missing allowing the browser and proxies to cache content.</p>
  
  
  
* URL: [https://snippets.cdn.mozilla.net/us-west/bundles-pregen/Firefox/en-us/default.json](https://snippets.cdn.mozilla.net/us-west/bundles-pregen/Firefox/en-us/default.json)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `max-age=600`
  
  
  
  
Instances: 1
  
### Solution
<p>Whenever possible ensure the cache-control HTTP header is set with no-cache, no-store, must-revalidate; and that the pragma HTTP header is set with no-cache.</p>
  
### Reference
* https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching

  
#### CWE Id : 525
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Incomplete or No Cache-control and Pragma HTTP Header Set
##### Low (Medium)
  
  
  
  
#### Description
<p>The cache-control and pragma HTTP header have not been set properly or are missing allowing the browser and proxies to cache content.</p>
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  
  
Instances: 1
  
### Solution
<p>Whenever possible ensure the cache-control HTTP header is set with no-cache, no-store, must-revalidate; and that the pragma HTTP header is set with no-cache.</p>
  
### Reference
* https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching

  
#### CWE Id : 525
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Incomplete or No Cache-control and Pragma HTTP Header Set
##### Low (Medium)
  
  
  
  
#### Description
<p>The cache-control and pragma HTTP header have not been set properly or are missing allowing the browser and proxies to cache content.</p>
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D1](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D1)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Home/Forum](https://localhost:44372/Home/Forum)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FAdmin%2FIndex](https://localhost:44372/Account/Login?ReturnUrl=%2FAdmin%2FIndex)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/css/site.css](https://localhost:44372/css/site.css)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D2](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D2)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/References/OnlineMedia](https://localhost:44372/References/OnlineMedia)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FForum](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FForum)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Home/Overview](https://localhost:44372/Home/Overview)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D1](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D1)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D4](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D4)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Register](https://localhost:44372/Account/Register)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D3](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D3)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Home/Reply?forumId=6](https://localhost:44372/Home/Reply?forumId=6)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Home/Forum](https://localhost:44372/Home/Forum)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Register](https://localhost:44372/Account/Register)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D2](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D2)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D3](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D3)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D6](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D6)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://localhost:44372/References](https://localhost:44372/References)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
  
* URL: [https://localhost:44372/Home/ForumPosts](https://localhost:44372/Home/ForumPosts)
  
  
  * Method: `POST`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
Instances: 37
  
### Solution
<p>Whenever possible ensure the cache-control HTTP header is set with no-cache, no-store, must-revalidate; and that the pragma HTTP header is set with no-cache.</p>
  
### Reference
* https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching

  
#### CWE Id : 525
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Incomplete or No Cache-control and Pragma HTTP Header Set
##### Low (Medium)
  
  
  
  
#### Description
<p>The cache-control and pragma HTTP header have not been set properly or are missing allowing the browser and proxies to cache content.</p>
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Parameter: `Pragma`
  
  
  * Evidence: `cache`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `s-maxage=900,public`
  
  
  
  
Instances: 2
  
### Solution
<p>Whenever possible ensure the cache-control HTTP header is set with no-cache, no-store, must-revalidate; and that the pragma HTTP header is set with no-cache.</p>
  
### Reference
* https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching

  
#### CWE Id : 525
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Incomplete or No Cache-control and Pragma HTTP Header Set
##### Low (Medium)
  
  
  
  
#### Description
<p>The cache-control and pragma HTTP header have not been set properly or are missing allowing the browser and proxies to cache content.</p>
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/plugins?_expected=1603126502200](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/plugins?_expected=1603126502200)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/search-default-override-allowlist?_expected=1595254618540](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/search-default-override-allowlist?_expected=1595254618540)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/top-sites?_expected=1611838808382](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/top-sites?_expected=1611838808382)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/password-recipes?_expected=1600889167888](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/password-recipes?_expected=1600889167888)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/anti-tracking-url-decoration?_expected=1564511755134](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/anti-tracking-url-decoration?_expected=1564511755134)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=whats-new-panel&bucket=main](https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=whats-new-panel&bucket=main)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `max-age=60`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/language-dictionaries?_expected=1569410800356](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/language-dictionaries?_expected=1569410800356)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=message-groups&bucket=main](https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=message-groups&bucket=main)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `max-age=60`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=cfr&bucket=main](https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=cfr&bucket=main)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `max-age=60`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/whats-new-panel/changeset?_expected=1611670765047](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/whats-new-panel/changeset?_expected=1611670765047)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/pioneer-study-addons-v1/changeset?_expected=1607042143590](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/pioneer-study-addons-v1/changeset?_expected=1607042143590)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=partitioning-exempt-urls&bucket=main](https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=partitioning-exempt-urls&bucket=main)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `max-age=60`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613327898738&_since=%221612658267016%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613327898738&_since=%221612658267016%22)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/partitioning-exempt-urls/changeset?_expected=1592906663254](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/partitioning-exempt-urls/changeset?_expected=1592906663254)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/cfr/changeset?_expected=1612312953148](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/cfr/changeset?_expected=1612312953148)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/gfx?_expected=1606146402211](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/gfx?_expected=1606146402211)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/message-groups/changeset?_expected=1595616291726](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/message-groups/changeset?_expected=1595616291726)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records](https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `max-age=60`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=fxmonitor-breaches&bucket=main](https://firefox.settings.services.mozilla.com/v1/buckets/monitor/collections/changes/records?collection=fxmonitor-breaches&bucket=main)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `max-age=60`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/hijack-blocklists?_expected=1605801189258](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/hijack-blocklists?_expected=1605801189258)
  
  
  * Method: `GET`
  
  
  * Parameter: `Cache-Control`
  
  
  * Evidence: `no-cache, no-store`
  
  
  
  
Instances: 29
  
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
  
  
  
* URL: [https://localhost:44372/TimeLine/Quiz](https://localhost:44372/TimeLine/Quiz)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Home/Reply?forumId=2](https://localhost:44372/Home/Reply?forumId=2)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FAdmin%2FIndex](https://localhost:44372/Account/LogIn?returnUrl=%2FAdmin%2FIndex)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D6](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D6)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/](https://localhost:44372/)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Admin/Index](https://localhost:44372/Admin/Index)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Home/Reply?forumId=1](https://localhost:44372/Home/Reply?forumId=1)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Admin](https://localhost:44372/Admin)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/TimeLine/Quiz](https://localhost:44372/TimeLine/Quiz)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/lib/jquery/dist/jquery.min.js](https://localhost:44372/lib/jquery/dist/jquery.min.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D1](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D1)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D2](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D2)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FAdmin%2FIndex](https://localhost:44372/Account/Login?ReturnUrl=%2FAdmin%2FIndex)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Home/Forum](https://localhost:44372/Home/Forum)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/css/site.css](https://localhost:44372/css/site.css)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D3](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D3)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D1](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D1)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/LogOut](https://localhost:44372/Account/LogOut)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Home/Forum](https://localhost:44372/Home/Forum)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [https://localhost:44372/Account/Register](https://localhost:44372/Account/Register)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
Instances: 51
  
### Solution
<p>Ensure that your web server, application server, load balancer, etc. is configured to suppress "X-Powered-By" headers.</p>
  
### Reference
* http://blogs.msdn.com/b/varunm/archive/2013/04/23/remove-unwanted-http-response-headers.aspx
* http://www.troyhunt.com/2012/02/shhh-dont-let-your-response-headers.html

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### X-Content-Type-Options Header Missing
##### Low (Medium)
  
  
  
  
#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>
  
  
  
* URL: [https://ftp.mozilla.org/pub/system-addons/reset-search-defaults/reset-search-defaults@mozilla.com-1.0.3-signed.xpi](https://ftp.mozilla.org/pub/system-addons/reset-search-defaults/reset-search-defaults@mozilla.com-1.0.3-signed.xpi)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
Instances: 1
  
### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>
  
### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>
  
### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
  
### X-Content-Type-Options Header Missing
##### Low (Medium)
  
  
  
  
#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>
  
  
  
* URL: [https://snippets.cdn.mozilla.net/us-west/bundles-pregen/Firefox/en-us/default.json](https://snippets.cdn.mozilla.net/us-west/bundles-pregen/Firefox/en-us/default.json)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
Instances: 1
  
### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>
  
### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>
  
### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
  
### X-Content-Type-Options Header Missing
##### Low (Medium)
  
  
  
  
#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-tracking-protection-facebook-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/social-tracking-protection-facebook-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/block-flash-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/block-flash-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/content-track-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/content-track-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/analytics-track-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/analytics-track-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/except-flashallow-digest256/1490633678](https://tracking-protection.cdn.mozilla.net/except-flashallow-digest256/1490633678)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/except-flashsubdoc-digest256/1517935265](https://tracking-protection.cdn.mozilla.net/except-flashsubdoc-digest256/1517935265)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/ads-track-digest256/1611614019](https://tracking-protection.cdn.mozilla.net/ads-track-digest256/1611614019)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/google-trackwhite-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/google-trackwhite-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-tracking-protection-twitter-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/social-tracking-protection-twitter-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/block-flashsubdoc-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/block-flashsubdoc-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-tracking-protection-linkedin-digest256/1564526481](https://tracking-protection.cdn.mozilla.net/social-tracking-protection-linkedin-digest256/1564526481)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/mozstd-trackwhite-digest256/1611614019](https://tracking-protection.cdn.mozilla.net/mozstd-trackwhite-digest256/1611614019)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/base-cryptomining-track-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/base-cryptomining-track-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-track-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/social-track-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/except-flash-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/except-flash-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/base-fingerprinting-track-digest256/1608186823](https://tracking-protection.cdn.mozilla.net/base-fingerprinting-track-digest256/1608186823)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/allow-flashallow-digest256/1490633678](https://tracking-protection.cdn.mozilla.net/allow-flashallow-digest256/1490633678)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
Instances: 17
  
### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>
  
### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>
  
### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
  
### X-Content-Type-Options Header Missing
##### Low (Medium)
  
  
  
  
#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>
  
  
  
* URL: [https://content-signature-2.cdn.mozilla.net/chains/remote-settings.content-signature.mozilla.org-2021-03-22-17-58-04.chain](https://content-signature-2.cdn.mozilla.net/chains/remote-settings.content-signature.mozilla.org-2021-03-22-17-58-04.chain)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
Instances: 1
  
### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>
  
### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>
  
### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
  
### X-Content-Type-Options Header Missing
##### Low (Medium)
  
  
  
  
#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
Instances: 1
  
### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>
  
### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>
  
### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
  
### X-Content-Type-Options Header Missing
##### Low (Medium)
  
  
  
  
#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>
  
  
  
* URL: [https://localhost:44372/TimeLine/Quiz](https://localhost:44372/TimeLine/Quiz)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D1](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D1)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/lib/jquery/dist/jquery.min.js](https://localhost:44372/lib/jquery/dist/jquery.min.js)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/TimeLine/Quiz](https://localhost:44372/TimeLine/Quiz)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FAdmin%2FIndex](https://localhost:44372/Account/LogIn?returnUrl=%2FAdmin%2FIndex)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D2](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D2)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D1](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D1)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FAdmin%2FIndex](https://localhost:44372/Account/Login?ReturnUrl=%2FAdmin%2FIndex)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/lib/bootstrap/dist/js/bootstrap.bundle.min.js](https://localhost:44372/lib/bootstrap/dist/js/bootstrap.bundle.min.js)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FForum](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FForum)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Home/Forum](https://localhost:44372/Home/Forum)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/Register](https://localhost:44372/Account/Register)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D2](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D2)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D3](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D3)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Home/Forum](https://localhost:44372/Home/Forum)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/css/site.css](https://localhost:44372/css/site.css)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Home/Overview](https://localhost:44372/Home/Overview)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D3](https://localhost:44372/Account/LogIn?returnUrl=%2FHome%2FReply%3FforumId%3D3)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/References/OnlineMedia](https://localhost:44372/References/OnlineMedia)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D4](https://localhost:44372/Account/Login?ReturnUrl=%2FHome%2FReply%3FforumId%3D4)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
Instances: 40
  
### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>
  
### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>
  
### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
  
### X-Content-Type-Options Header Missing
##### Low (Medium)
  
  
  
  
#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
Instances: 1
  
### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>
  
### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>
  
### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
  
### X-Content-Type-Options Header Missing
##### Low (Medium)
  
  
  
  
#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>
  
  
  
* URL: [https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2](https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
Instances: 1
  
### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>
  
### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scan rule will not alert on client or server error responses.</p>
  
### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://owasp.org/www-community/Security_Headers

  
#### CWE Id : 16
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
  
### Charset Mismatch 
##### Informational (Low)
  
  
  
  
#### Description
<p>This check identifies responses where the HTTP Content-Type header declares a charset different from the charset defined by the body of the HTML or XML. When there's a charset mismatch between the HTTP header and content body Web browsers can be forced into an undesirable content-sniffing mode to determine the content's correct character set.</p><p></p><p>An attacker could manipulate content on the page to be interpreted in an encoding of their choice. For example, if an attacker can control content at the beginning of the page, they could inject script using UTF-7 encoded text and manipulate some browsers into interpreting that text.</p>
  
  
  
* URL: [https://aus5.mozilla.org/update/3/SystemAddons/85.0.2/20210208133944/WINNT_x86_64-msvc-x64/en-US/release/Windows_NT%2010.0.0.0.19041.804%20(x64)/default/default/update.xml](https://aus5.mozilla.org/update/3/SystemAddons/85.0.2/20210208133944/WINNT_x86_64-msvc-x64/en-US/release/Windows_NT%2010.0.0.0.19041.804%20(x64)/default/default/update.xml)
  
  
  * Method: `GET`
  
  
  
  
Instances: 1
  
### Solution
<p>Force UTF-8 for all text content in both the HTTP header and meta tags in HTML or encoding declarations in XML.</p>
  
### Other information
<p>There was a charset mismatch between the HTTP Header and the XML encoding declaration: [utf-8] and [null] do not match.</p>
  
### Reference
* http://code.google.com/p/browsersec/wiki/Part2#Character_set_handling_and_detection

  
#### CWE Id : 16
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
  
### Information Disclosure - Suspicious Comments
##### Informational (Medium)
  
  
  
  
#### Description
<p>The response appears to contain suspicious comments which may help an attacker. Note: Matches made within script blocks or files are against the entire content not only comments.</p>
  
  
  
* URL: [https://localhost:44372/](https://localhost:44372/)
  
  
  * Method: `GET`
  
  
  * Evidence: `Admin`
  
  
  
  
Instances: 1
  
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
  
  
  
* URL: [https://localhost:44372/Account/LogIn](https://localhost:44372/Account/LogIn)
  
  
  * Method: `POST`
  
  
  
  
* URL: [https://localhost:44372/Account/Register](https://localhost:44372/Account/Register)
  
  
  * Method: `GET`
  
  
  
  
* URL: [https://localhost:44372/Account/LogOut](https://localhost:44372/Account/LogOut)
  
  
  * Method: `GET`
  
  
  
  
* URL: [https://localhost:44372/Account/Register](https://localhost:44372/Account/Register)
  
  
  * Method: `GET`
  
  
  
  
* URL: [https://localhost:44372/Account/Login](https://localhost:44372/Account/Login)
  
  
  * Method: `GET`
  
  
  
  
* URL: [https://localhost:44372/Account/Register](https://localhost:44372/Account/Register)
  
  
  * Method: `POST`
  
  
  
  
Instances: 6
  
### Solution
<p>Always scope cookies to a FQDN (Fully Qualified Domain Name).</p>
  
### Other information
<p>The origin domain used for comparison was: </p><p>localhost</p><p>.AspNetCore.Identity.Application=CfDJ8J25h_GVVAlAudqFyu1vM_P7u9-gnK5n-9T-VVbLdFqEAZJj68rOd2kILOBFLFBZ_DxgQJjpodqL-IWoE04p54LzEuwFo7NQEQeO-2f8F7qfeG6AyS6WbdVVh8-1a-feVwKXKe2ZtcEMT23xkGCXs4aeHy8qD9ePuU1N9keeMAcpQVhNojGMSZJGBxYoWNQGJwcYYEIHDV1PiUXQmzgUGoXnhhZkAe2M88EFWX33kVqyCqSnUX8pSlttWQa1WqzqIM9sgH8OhBUsCHJUrBD37-6NPPsckLniWLfi0j8aHU-xjMB6RV9wHV1P52UZocukSIJNRHlfakyu7NUt43r9yHvjsy2-VgO9BX7_SzQGG1xtf4c6jxv6a7ig8eD2JiXL7ky26j0FUZVXgRYhJVXAl_FnT595ovA3EbX_UeFM4GmZxLcvCcQze5Vh9UeW3sHgyieQzmf5vnogfgfMSOeAuZZR4RdbLn94uYibZrow-_A7gv7_BVOgtLZLCPTrhvLgvJA4BkFkNfUw6vynUQY9K0SCuI88H4U8Hvk50JFQCtUOliblUp5XTwky0AdMlDUnGt-I3Dle2XBlIA6OhretjJOnzHggGRXfr-EgOs4zn90imL-s2dvVNbbcsPvETVUgllS_ayvlpAVYh9nsMTy7cujMyulhSaRK_x4B51qOxMLnZ4TLZPV9xYpZ-lIllJT7ZeOAnknUbKkcluC6HkjrfpQ</p><p></p>
  
### Reference
* https://tools.ietf.org/html/rfc6265#section-4.1
* https://owasp.org/www-project-web-security-testing-guide/v41/4-Web_Application_Security_Testing/06-Session_Management_Testing/02-Testing_for_Cookies_Attributes.html
* http://code.google.com/p/browsersec/wiki/Part2#Same-origin_policy_for_cookies

  
#### CWE Id : 565
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
  
### Timestamp Disclosure - Unix
##### Informational (Low)
  
  
  
  
#### Description
<p>A timestamp was disclosed by the application/web server - Unix</p>
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/block-flashsubdoc-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/block-flashsubdoc-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Evidence: `1604686195`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/allow-flashallow-digest256/1490633678](https://tracking-protection.cdn.mozilla.net/allow-flashallow-digest256/1490633678)
  
  
  * Method: `GET`
  
  
  * Evidence: `1490633678`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/except-flashallow-digest256/1490633678](https://tracking-protection.cdn.mozilla.net/except-flashallow-digest256/1490633678)
  
  
  * Method: `GET`
  
  
  * Evidence: `1490633678`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/base-cryptomining-track-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/base-cryptomining-track-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Evidence: `1604686195`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-tracking-protection-linkedin-digest256/1564526481](https://tracking-protection.cdn.mozilla.net/social-tracking-protection-linkedin-digest256/1564526481)
  
  
  * Method: `GET`
  
  
  * Evidence: `1564526481`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/except-flash-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/except-flash-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Evidence: `1604686195`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-tracking-protection-facebook-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/social-tracking-protection-facebook-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Evidence: `1604686195`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/except-flashsubdoc-digest256/1517935265](https://tracking-protection.cdn.mozilla.net/except-flashsubdoc-digest256/1517935265)
  
  
  * Method: `GET`
  
  
  * Evidence: `1517935265`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/ads-track-digest256/1611614019](https://tracking-protection.cdn.mozilla.net/ads-track-digest256/1611614019)
  
  
  * Method: `GET`
  
  
  * Evidence: `1611614019`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/analytics-track-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/analytics-track-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Evidence: `1604686195`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/block-flash-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/block-flash-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Evidence: `1604686195`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/mozstd-trackwhite-digest256/1611614019](https://tracking-protection.cdn.mozilla.net/mozstd-trackwhite-digest256/1611614019)
  
  
  * Method: `GET`
  
  
  * Evidence: `1611614019`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-tracking-protection-twitter-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/social-tracking-protection-twitter-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Evidence: `1604686195`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/base-fingerprinting-track-digest256/1608186823](https://tracking-protection.cdn.mozilla.net/base-fingerprinting-track-digest256/1608186823)
  
  
  * Method: `GET`
  
  
  * Evidence: `1608186823`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/social-track-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/social-track-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Evidence: `1604686195`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/content-track-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/content-track-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Evidence: `1604686195`
  
  
  
  
* URL: [https://tracking-protection.cdn.mozilla.net/google-trackwhite-digest256/1604686195](https://tracking-protection.cdn.mozilla.net/google-trackwhite-digest256/1604686195)
  
  
  * Method: `GET`
  
  
  * Evidence: `1604686195`
  
  
  
  
Instances: 17
  
### Solution
<p>Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.</p>
  
### Other information
<p>1604686195, which evaluates to: 2020-11-06 10:09:55</p>
  
### Reference
* http://projects.webappsec.org/w/page/13246936/Information%20Leakage

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Timestamp Disclosure - Unix
##### Informational (Low)
  
  
  
  
#### Description
<p>A timestamp was disclosed by the application/web server - Unix</p>
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `97123117`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15010262`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15077976`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22657590`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15078719`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22740310`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22657919`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15050987`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22756457`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15034676`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15077945`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22655253`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15077893`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22639566`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22654524`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15051038`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22716435`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22734312`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `22756959`
  
  
  
  
* URL: [https://spocs.getpocket.com/spocs](https://spocs.getpocket.com/spocs)
  
  
  * Method: `POST`
  
  
  * Evidence: `15084196`
  
  
  
  
Instances: 106
  
### Solution
<p>Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.</p>
  
### Other information
<p>97123117, which evaluates to: 1973-01-28 18:38:37</p>
  
### Reference
* http://projects.webappsec.org/w/page/13246936/Information%20Leakage

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Timestamp Disclosure - Unix
##### Informational (Low)
  
  
  
  
#### Description
<p>A timestamp was disclosed by the application/web server - Unix</p>
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1612994477`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `114447077`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1612952660`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1612898278`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613109635`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `108191256`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1846252749`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1612346400`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1612972562`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613041200`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1611900000`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1612972817`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `508235087`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1211002020`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1143340157`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1612987200`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613023200`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1556051316`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1612977733`
  
  
  
  
* URL: [https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30](https://getpocket.cdn.mozilla.net/v3/firefox/global-recs?version=3&consumer_key=40249-e88c401e1b1f2242d9e441c4&locale_lang=en-US&region=US&count=30)
  
  
  * Method: `GET`
  
  
  * Evidence: `1613131255`
  
  
  
  
Instances: 28
  
### Solution
<p>Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.</p>
  
### Other information
<p>1612994477, which evaluates to: 2021-02-10 14:01:17</p>
  
### Reference
* http://projects.webappsec.org/w/page/13246936/Information%20Leakage

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Timestamp Disclosure - Unix
##### Informational (Low)
  
  
  
  
#### Description
<p>A timestamp was disclosed by the application/web server - Unix</p>
  
  
  
* URL: [https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2](https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2)
  
  
  * Method: `POST`
  
  
  * Evidence: `1490633678`
  
  
  
  
* URL: [https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2](https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2)
  
  
  * Method: `POST`
  
  
  * Evidence: `1564526481`
  
  
  
  
* URL: [https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2](https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2)
  
  
  * Method: `POST`
  
  
  * Evidence: `1517935265`
  
  
  
  
* URL: [https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2](https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2)
  
  
  * Method: `POST`
  
  
  * Evidence: `1611614019`
  
  
  
  
* URL: [https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2](https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2)
  
  
  * Method: `POST`
  
  
  * Evidence: `1604686195`
  
  
  
  
* URL: [https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2](https://shavar.services.mozilla.com/downloads?client=navclient-auto-ffox&appver=85.0&pver=2.2)
  
  
  * Method: `POST`
  
  
  * Evidence: `1608186823`
  
  
  
  
Instances: 6
  
### Solution
<p>Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.</p>
  
### Other information
<p>1490633678, which evaluates to: 2017-03-27 09:54:38</p>
  
### Reference
* http://projects.webappsec.org/w/page/13246936/Information%20Leakage

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
  
### Timestamp Disclosure - Unix
##### Informational (Low)
  
  
  
  
#### Description
<p>A timestamp was disclosed by the application/web server - Unix</p>
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `36395491`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `11657763`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613327898738&_since=%221612658267016%22](https://firefox.settings.services.mozilla.com/v1/buckets/blocklists/collections/addons-bloomfilters/changeset?_expected=1613327898738&_since=%221612658267016%22)
  
  
  * Method: `GET`
  
  
  * Evidence: `77063849`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `29020808`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `20580060`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `47391665`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `14610585`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `48881308`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `40767652`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `12218087`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `15453048`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `40960499`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `28510459`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `24853850`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `44320330`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `28364826`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `91890110`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `22437749`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `86591957`
  
  
  
  
* URL: [https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647](https://firefox.settings.services.mozilla.com/v1/buckets/main/collections/fxmonitor-breaches/changeset?_expected=1612303475647)
  
  
  * Method: `GET`
  
  
  * Evidence: `26183992`
  
  
  
  
Instances: 104
  
### Solution
<p>Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.</p>
  
### Other information
<p>36395491, which evaluates to: 1971-02-25 21:51:31</p>
  
### Reference
* http://projects.webappsec.org/w/page/13246936/Information%20Leakage

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3
