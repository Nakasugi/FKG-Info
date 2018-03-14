var getDMaster = "http://web.flower-knight-girls.co.jp/api/v1/master/getMaster";
var getNMaster = "https://rn-ssl.floweknightgirl.com/api/v1/master/getMaster";

var reqHeaders;
var reqMessage;
var req;
var reqURL;



function LogBeforeReq(requestDetails)
{
	if (requestDetails.method != 'POST') return;
	chrome.webRequest.onBeforeRequest.removeListener(LogBeforeReq);
	
	console.log(JSON.stringify(requestDetails));
	reqURL = requestDetails.url;
	reqMessage = "";
	var dv = new DataView(requestDetails.requestBody.raw[0].bytes);
	for(var j = 0; j < dv.byteLength; j++) reqMessage += String.fromCharCode(dv.getInt8(j));

	console.log("Send: " + reqMessage);
}



function LogBeforeSendHead(requestDetails)
{
	if (requestDetails.method != 'POST') return;
	chrome.webRequest.onBeforeSendHeaders.removeListener(LogBeforeSendHead);
	
	reqHeaders = requestDetails.requestHeaders;
	
	console.log(JSON.stringify(reqHeaders));
	
	reqPostSend();
}



chrome.webRequest.onBeforeRequest.addListener(LogBeforeReq, {urls:[getDMaster, getNMaster]}, ["requestBody"]);
chrome.webRequest.onBeforeSendHeaders.addListener(LogBeforeSendHead, {urls:[getDMaster, getNMaster]}, ["requestHeaders"]);



function reqPostSend()
{
	req = new XMLHttpRequest();
	req.open("POST", reqURL, true);
	req.responseType = "arraybuffer";
	req.onload = reqPostRecive;
	
	for (var i = 0; i < reqHeaders.length; i++) req.setRequestHeader(reqHeaders[i].name, reqHeaders[i].value);
	
	req.send(reqMessage);
}



function reqPostRecive()
{
	if (this.response)
	{
		console.log("Status: " + this.status);
		console.log("Recive: " + this.response.length);
		
		var mout = new XMLHttpRequest();
		mout.open("POST", "http://127.0.0.1:8778", true);
		
		mout.upload.onload = function(){ mout.abort(); }
		
		mout.send(this.response);
		
		
		//SaveToFile(this.responseText);
	}
}


