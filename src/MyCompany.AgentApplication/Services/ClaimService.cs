using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using MyCompany.AgentApplication.Configuration;
using MyCompany.AgentApplication.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.AgentApplication.Services
{
    public class ClaimService : IClaimService
    {
        private readonly HttpClient _httpClient;
        private readonly IClaimServiceApiConfig _claimServiceApiConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimService(HttpClient httpClient, IClaimServiceApiConfig claimServiceApiConfig, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _claimServiceApiConfig = claimServiceApiConfig ?? throw new ArgumentNullException(nameof(claimServiceApiConfig));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            var accessToken = _httpContextAccessor.HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task<ClaimDTO> AddAsync(ClaimDTO claim)
        {
            var response = await _httpClient.PostAsync($"{_claimServiceApiConfig.BaseAddress}/claims", new StringContent(JsonConvert.SerializeObject(claim), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            var createdClaim = JsonConvert.DeserializeObject<ClaimDTO>(jsonResult);

            return createdClaim;
        }

        public async Task<bool> DeleteAsync(int claimId)
        {
            var result = await _httpClient.DeleteAsync($"{_claimServiceApiConfig.BaseAddress}/Claims/{ claimId}");
            return result.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ClaimDTO>> GetAllClaims(string keyword = null)
        {
            return JsonConvert.DeserializeObject<List<ClaimDTO>>(await _httpClient.GetStringAsync($"{_claimServiceApiConfig.BaseAddress}/Claims"));
        }

        public async Task<ClaimDTO> GetAsync(int claimId)
        {
            return JsonConvert.DeserializeObject<ClaimDTO>(await _httpClient.GetStringAsync($"{_claimServiceApiConfig.BaseAddress}/Claims/{claimId}"));
        }

        public async Task<bool> UpdateAsync(ClaimDTO claim)
        {
            var result = await _httpClient.PutAsync($"{_claimServiceApiConfig.BaseAddress}/Claims/{claim.ClaimId}", new StringContent(JsonConvert.SerializeObject(claim), Encoding.UTF8, "application/json"));
            return result.IsSuccessStatusCode;
        }
    }
}