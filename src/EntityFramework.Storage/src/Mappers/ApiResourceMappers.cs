/*
 Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using IdentityServer8.EntityFramework.Entities;

namespace IdentityServer8.EntityFramework.Mappers
{
    /// <summary>
    /// Extension methods to map to/from entity/model for API resources.
    /// </summary>
    public static class ApiResourceMappers
    {
        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.ApiResource ToModel(this ApiResource entity)
        {
            if (entity == null) return null;

            return new Models.ApiResource
            {
                Enabled = entity.Enabled,
                Name = entity.Name,
                DisplayName = entity.DisplayName,
                Description = entity.Description,
                ShowInDiscoveryDocument = entity.ShowInDiscoveryDocument,
                AllowedAccessTokenSigningAlgorithms = AllowedSigningAlgorithmsConverter.Convert(entity.AllowedAccessTokenSigningAlgorithms),
                UserClaims = entity.UserClaims?.Select(c => c.Type).ToHashSet() ?? new HashSet<string>(),
                Properties = entity.Properties?.ToDictionary(p => p.Key, p => p.Value) ?? new Dictionary<string, string>(),
                ApiSecrets = entity.Secrets?.Select(s => new Models.Secret
                {
                    Description = s.Description,
                    Value = s.Value,
                    Expiration = s.Expiration,
                    Type = s.Type,
                }).ToHashSet() ?? new HashSet<Models.Secret>(),
                Scopes = entity.Scopes?.Select(s => s.Scope).ToHashSet() ?? new HashSet<string>(),
            };
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static ApiResource ToEntity(this Models.ApiResource model)
        {
            if (model == null) return null;

            return new ApiResource
            {
                Enabled = model.Enabled,
                Name = model.Name,
                DisplayName = model.DisplayName,
                Description = model.Description,
                ShowInDiscoveryDocument = model.ShowInDiscoveryDocument,
                AllowedAccessTokenSigningAlgorithms = AllowedSigningAlgorithmsConverter.Convert(model.AllowedAccessTokenSigningAlgorithms),
                UserClaims = model.UserClaims?.Select(c => new ApiResourceClaim { Type = c }).ToList() ?? new List<ApiResourceClaim>(),
                Properties = model.Properties?.Select(p => new ApiResourceProperty { Key = p.Key, Value = p.Value }).ToList() ?? new List<ApiResourceProperty>(),
                Secrets = model.ApiSecrets?.Select(s => new ApiResourceSecret
                {
                    Description = s.Description,
                    Value = s.Value,
                    Expiration = s.Expiration,
                    Type = s.Type,
                }).ToList() ?? new List<ApiResourceSecret>(),
                Scopes = model.Scopes?.Select(s => new ApiResourceScope { Scope = s }).ToList() ?? new List<ApiResourceScope>(),
            };
        }
    }
}
