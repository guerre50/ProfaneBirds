// http://supersegfault.com/accessing-google-services-via-c/

using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
 
public class UnsafeSecurityPolicy {
 
	public static bool Validator(
	    object sender,
	    X509Certificate certificate,
	    X509Chain chain,
	    SslPolicyErrors policyErrors) {
	 
	    //*** Just accept and move on...
	    Debug.Log ("Validation successful!");
	    return true;   
	}
	 
	public static void Instate() {
	 
	    ServicePointManager.ServerCertificateValidationCallback = Validator;
	}
}