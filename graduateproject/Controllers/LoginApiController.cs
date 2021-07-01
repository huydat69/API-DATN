using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GraduateProject.Controllers
{
    public class LoginApiController : ApiController
    {
        [HttpPost]
        [Route("api/login")]
        public async Task<IHttpActionResult> PostLogin([FromBody] JObject data)
        {
            if (!ModelState.IsValid || data["username"] == null || data["password"] == null)
            {
                return BadRequest(ModelState);
            }

            using (var client = new HttpClient())
            {
                try
                {
                    Dictionary<string, string> lst = new Dictionary<string, string>
                    {
                        { "grant_type", "password" },
                        { "username", data["username"].ToString() },
                        { "password", data["password"].ToString() }
                    };
                    NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(String.Empty);
                    foreach (var item in lst)
                    {
                        outgoingQueryString.Add(item.Key, item.Value);
                    }

                    StringContent content = new StringContent(outgoingQueryString.ToString());
                    string baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                    if (!baseUrl.EndsWith("/")) baseUrl = string.Format("{0}/", baseUrl);
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    var response = await client.PostAsync(string.Format("{0}{1}", baseUrl, "token123321"), content);
                    var authResult = await response.Content.ReadAsStringAsync();

                    if (authResult.ToUpper().Contains("invalid_grant".ToUpper()))
                    {
                        return Ok(new ApiResult() { success = false, message = "Sai tên đăng nhập hoặc mật khẩu!", data = null });
                    }
                    else if (authResult.ToUpper().Contains("\"error\"".ToUpper()))
                    {
                        throw new Exception();
                    }
                    else if (authResult.ToUpper().Contains("access_token".ToUpper()))
                    {
                        JObject tokenObject = JObject.Parse(authResult);
                        JObject resultObject = new JObject();
                        resultObject["token"] = tokenObject["access_token"].ToString();
                        resultObject["token_type"] = tokenObject["token_type"].ToString();
                        resultObject["expires_in"] = tokenObject["expires_in"];
                        resultObject["refresh_token"] = tokenObject["refresh_token"].ToString();
                        if (tokenObject["user"] != null && !string.IsNullOrWhiteSpace(tokenObject["user"].ToString()))
                            resultObject["user"] = JObject.Parse(tokenObject["user"].ToString());

                        ApiResult apiResult = new ApiResult() { success = true, message = "Đăng nhập thành công!", data = resultObject };
                        return Ok(apiResult);
                    }

                    return Ok(authResult);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
