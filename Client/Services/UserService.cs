using Microsoft.AspNetCore.Components;
using BlazorSignalRApp.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Linq.Expressions;

namespace BlazorSignalRApp.Client.Services
{
    public class UserService
    {
        public UserService(HttpClient httpClient, ISyncLocalStorageService localStorage)
        {
            _http = httpClient;
            _localstorage = localStorage;
        }
        public HttpClient _http { get; }
        private ISyncLocalStorageService _localstorage { get; }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _http.GetJsonAsync<List<User>>("api/Users");
        }

        public async Task<User> GetUserAsync()
        {
            if (_localstorage.ContainKey("User"))
            {
                var id = _localstorage.GetItem<string>("User");
                try
                {
                    return await _http.GetJsonAsync<User>("api/Users/" + id);
                }
                catch (Exception ex)
                {
                }
            }
            return new User();
        }

        public async Task<bool> CheckUserAsync()
        {
            if (_localstorage.ContainKey("User"))
            {
                var id = _localstorage.GetItem<string>("User");
                try
                {
                    await _http.GetJsonAsync<User>("api/Users/" + id);
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public async Task PutUserByIdAsync(User _user)
        {
            _localstorage.SetItem("User", _user.UserId);
            await _http.PutJsonAsync("api/Users/" + _user.UserId, _user);
        }

        public async Task DeleteUserByIdAsync(User _user)
        {
            await _http.DeleteAsync("api/Users/" + _user.UserId);
        }

        public async Task PostUsersAsync(User _user)
        {
            _localstorage.SetItem("User", _user.UserId);
            await _http.PostJsonAsync<User>("api/Users/", _user);
        }
    }
}
