using RestSharp;
using System.Text.Json;

namespace Signagelive
{
    public class Signagelive
    {
        string? baseUrl;
        string? clientId;
        string? clientSecret;
        string? networkId;
        string? authorizationCode;

        public Signagelive() { }
        public Signagelive(string BaseUrl, string ClientId, string ClientSecret, string NetworkId, string AuthorizationCode)
        {
            baseUrl = BaseUrl;
            clientId = ClientId;
            clientSecret = ClientSecret;
            networkId = NetworkId;
            authorizationCode = AuthorizationCode;
        }

        public Task<Mediaasset?> UploadAssetAsync(string accessToken, string name, string type, byte[] bytes)
        {
            Mediaasset? mediaasset;
            string request_resource = string.Format("networks/{0}/{1}", networkId.ToString(), "upload");

            RestClient restClient = new RestClient(baseUrl);
            RestRequest restRequest = new RestRequest(request_resource, Method.Post);
            restRequest.AddHeader("Authorization", string.Concat("bearer", " ", accessToken));
            restRequest.AddHeader("Content-Type", "multipart/form-data");
            restRequest.AddFile(name, bytes, name, type);
            RestResponse response = restClient.Execute(restRequest);

            if (response.IsSuccessful && response != null && response.Content != null)
            {
                mediaasset = (Mediaasset?)JsonSerializer.Deserialize(response.Content, typeof(Mediaasset));
                return Task.FromResult(mediaasset);
            }
            return Task.FromResult((Mediaasset?)null);
        }
    
        public Task<bool> DeletePlaylistAsync(string accessToken, int playlistId)
        {
            string request_resource = string.Format("networks/{0}/{1}/{2}", networkId.ToString(), "playlists", playlistId.ToString());

            RestClient restClient = new RestClient(baseUrl);
            RestRequest restRequest = new RestRequest(request_resource, Method.Delete);
            restRequest.AddHeader("Authorization", string.Concat("bearer", " ", accessToken));
            restRequest.AddParameter("Content-Type", "application/json");

            RestResponse response = restClient.Execute(restRequest);

            if (response.IsSuccessful && response != null && response.Content != null)
            {
                return Task.FromResult(true);
            }
            
            return Task.FromResult(false);
        }

        public Task<Playlist?> CreatePlaylistAsync(string accessToken, string playlistName)
        {
            Playlist? playlist;

            string request_resource = string.Format("networks/{0}/{1}", networkId.ToString(), "Playlists");

            RestClient restClient = new RestClient(baseUrl);
            RestRequest restRequest = new RestRequest(request_resource, Method.Post);
            restRequest.AddHeader("Authorization", string.Concat("bearer", " ", accessToken));
            restRequest.AddHeader("Content-Type", "application/json");

            var ob = new { name = playlistName };
            string name = JsonSerializer.Serialize(ob);

            restRequest.AddJsonBody(name);

            RestResponse response = restClient.Execute(restRequest);

            if (response.IsSuccessful && response != null && response.Content != null)
            {
                playlist = (Playlist?)JsonSerializer.Deserialize(response.Content, typeof(Playlist));
                return Task.FromResult(playlist);
            }
            return Task.FromResult((Playlist?)null);
        }
        public Task<Playlist?> GetPlaylistAsync(string accessToken, int playlistId)
        {
            Playlist? playlist;

            string request_resource = string.Format("networks/{0}/{1}/{2}", networkId, "Playlists", playlistId.ToString());

            RestClient restClient = new RestClient(baseUrl);
            RestRequest restRequest = new RestRequest(request_resource, Method.Get);
            restRequest.AddHeader("Authorization", string.Concat("bearer", " ", accessToken));
            restRequest.AddParameter("Content-Type", "application/json");

            RestResponse response = restClient.Execute(restRequest);

            if (response.IsSuccessful && response != null && response.Content != null)
            {
                playlist = (Playlist?)JsonSerializer.Deserialize(response.Content, typeof(Playlist));
                return Task.FromResult(playlist);
            }
            return Task.FromResult((Playlist?)null);
        }

        public Task<Playlist[]?> GetPlaylistsAsync(string accessToken, string search)
        {
            Playlist[]? playlists;

            string request_resource = string.Format("networks/{0}/{1}", networkId, "Playlists");

            RestClient restClient = new RestClient(baseUrl);
            RestRequest restRequest = new RestRequest(request_resource, Method.Get);
            restRequest.AddHeader("Authorization", string.Concat("bearer", " ", accessToken));
            restRequest.AddParameter("Content-Type", "application/json");
            restRequest.AddParameter("search", search, ParameterType.QueryString);

            RestResponse response = restClient.Execute(restRequest);

            if (response.IsSuccessful && response != null && response.Content != null)
            {
                playlists = (Playlist[]?)JsonSerializer.Deserialize(response.Content, typeof(Playlist[]));
                return Task.FromResult(playlists);
            }
            return Task.FromResult((Playlist[]?)null);
        }

        public Task<Token?> GetAccessTokenAsync()
        {
            Token? token;

            RestClient restClient = new(baseUrl);
            RestRequest restRequest = new("token", Method.Post);
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddParameter("grant_type", "authorization_code");
            restRequest.AddParameter("client_id", clientId);
            restRequest.AddParameter("client_secret", clientSecret);
            restRequest.AddParameter("code", authorizationCode);

            RestResponse response = restClient.Execute(restRequest);

            if (response.IsSuccessful && response != null && response.Content != null)
            {
                token = (Token?)JsonSerializer.Deserialize(response.Content, typeof(Token));
                return Task.FromResult(token);
            }
            return Task.FromResult((Token?)null);
        }

        public Task<Mediaasset?> GetMediaassetAsync(string accessToken, int mediaassetId) 
        {
            Mediaasset? mediaasset;

            string request_resource = string.Format("networks/{0}/{1}/{2}", networkId, "mediaassets", mediaassetId.ToString());

            RestClient restClient = new RestClient(baseUrl);
            RestRequest restRequest = new RestRequest(request_resource, Method.Get);
            restRequest.AddHeader("Authorization", string.Concat("bearer", " ", accessToken));
            restRequest.AddParameter("Content-Type", "application/json");

            RestResponse response = restClient.Execute(restRequest);

            if (response.IsSuccessful && response != null && response.Content != null)
            {
                mediaasset = (Mediaasset?)JsonSerializer.Deserialize(response.Content, typeof(Mediaasset));
                return Task.FromResult(mediaasset);
            }
            return Task.FromResult((Mediaasset?)null);
        }
    }

}