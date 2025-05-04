using Microsoft.AspNetCore.Mvc;

public static ContentResult JsonContentResult(string content,int statusCode)=> new ContentResult(){  Content=content,  StatusCode=statusCode, ContentType="application/json"};