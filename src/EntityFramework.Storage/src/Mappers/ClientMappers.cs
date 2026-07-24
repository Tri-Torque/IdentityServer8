/*
 Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

namespace IdentityServer8.EntityFramework.Mappers
{
    /// <summary>
    /// Extension methods to map to/from entity/model for clients.
    /// </summary>
    public static class ClientMappers
    {
        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.Client ToModel(this Entities.Client entity)
        {
            if (entity == null) return null;

            var model = new Models.Client
            {
                Enabled = entity.Enabled,
                ClientId = entity.ClientId,
                RequireClientSecret = entity.RequireClientSecret,
                ClientName = entity.ClientName,
                Description = entity.Description,
                ClientUri = entity.ClientUri,
                LogoUri = entity.LogoUri,
                RequireConsent = entity.RequireConsent,
                AllowRememberConsent = entity.AllowRememberConsent,
                AlwaysIncludeUserClaimsInIdToken = entity.AlwaysIncludeUserClaimsInIdToken,
                RequirePkce = entity.RequirePkce,
                AllowPlainTextPkce = entity.AllowPlainTextPkce,
                RequireRequestObject = entity.RequireRequestObject,
                AllowAccessTokensViaBrowser = entity.AllowAccessTokensViaBrowser,
                FrontChannelLogoutUri = entity.FrontChannelLogoutUri,
                FrontChannelLogoutSessionRequired = entity.FrontChannelLogoutSessionRequired,
                BackChannelLogoutUri = entity.BackChannelLogoutUri,
                BackChannelLogoutSessionRequired = entity.BackChannelLogoutSessionRequired,
                AllowOfflineAccess = entity.AllowOfflineAccess,
                IdentityTokenLifetime = entity.IdentityTokenLifetime,
                AllowedIdentityTokenSigningAlgorithms = AllowedSigningAlgorithmsConverter.Convert(entity.AllowedIdentityTokenSigningAlgorithms),
                AccessTokenLifetime = entity.AccessTokenLifetime,
                AuthorizationCodeLifetime = entity.AuthorizationCodeLifetime,
                ConsentLifetime = entity.ConsentLifetime,
                AbsoluteRefreshTokenLifetime = entity.AbsoluteRefreshTokenLifetime,
                SlidingRefreshTokenLifetime = entity.SlidingRefreshTokenLifetime,
                RefreshTokenUsage = (Models.TokenUsage)entity.RefreshTokenUsage,
                UpdateAccessTokenClaimsOnRefresh = entity.UpdateAccessTokenClaimsOnRefresh,
                RefreshTokenExpiration = (Models.TokenExpiration)entity.RefreshTokenExpiration,
                AccessTokenType = (Models.AccessTokenType)entity.AccessTokenType,
                EnableLocalLogin = entity.EnableLocalLogin,
                IncludeJwtId = entity.IncludeJwtId,
                AlwaysSendClientClaims = entity.AlwaysSendClientClaims,
                ClientClaimsPrefix = entity.ClientClaimsPrefix,
                PairWiseSubjectSalt = entity.PairWiseSubjectSalt,
                UserSsoLifetime = entity.UserSsoLifetime,
                UserCodeType = entity.UserCodeType,
                DeviceCodeLifetime = entity.DeviceCodeLifetime,
                AllowedGrantTypes = entity.AllowedGrantTypes?.Select(g => g.GrantType).ToHashSet() ?? new HashSet<string>(),
                RedirectUris = entity.RedirectUris?.Select(r => r.RedirectUri).ToHashSet() ?? new HashSet<string>(),
                PostLogoutRedirectUris = entity.PostLogoutRedirectUris?.Select(r => r.PostLogoutRedirectUri).ToHashSet() ?? new HashSet<string>(),
                AllowedScopes = entity.AllowedScopes?.Select(s => s.Scope).ToHashSet() ?? new HashSet<string>(),
                IdentityProviderRestrictions = entity.IdentityProviderRestrictions?.Select(r => r.Provider).ToHashSet() ?? new HashSet<string>(),
                AllowedCorsOrigins = entity.AllowedCorsOrigins?.Select(o => o.Origin).ToHashSet() ?? new HashSet<string>(),
                Claims = entity.Claims?.Select(c => new Models.ClientClaim(c.Type, c.Value, ClaimValueTypes.String)).ToHashSet() ?? new HashSet<Models.ClientClaim>(),
                Properties = entity.Properties?.ToDictionary(p => p.Key, p => p.Value) ?? new Dictionary<string, string>(),
                ClientSecrets = entity.ClientSecrets?.Select(s =>
                {
                    var secret = new Models.Secret
                    {
                        Description = s.Description,
                        Value = s.Value,
                        Expiration = s.Expiration,
                    };
                    // Only map Type if the source is not null (preserve model default)
                    if (s.Type != null)
                    {
                        secret.Type = s.Type;
                    }
                    return secret;
                }).ToHashSet() ?? new HashSet<Models.Secret>(),
            };

            // Only map ProtocolType if the source is not null (preserve model default)
            if (entity.ProtocolType != null)
            {
                model.ProtocolType = entity.ProtocolType;
            }

            return model;
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.Client ToEntity(this Models.Client model)
        {
            if (model == null) return null;

            var entity = new Entities.Client
            {
                Enabled = model.Enabled,
                ClientId = model.ClientId,
                RequireClientSecret = model.RequireClientSecret,
                ClientName = model.ClientName,
                Description = model.Description,
                ClientUri = model.ClientUri,
                LogoUri = model.LogoUri,
                RequireConsent = model.RequireConsent,
                AllowRememberConsent = model.AllowRememberConsent,
                AlwaysIncludeUserClaimsInIdToken = model.AlwaysIncludeUserClaimsInIdToken,
                RequirePkce = model.RequirePkce,
                AllowPlainTextPkce = model.AllowPlainTextPkce,
                RequireRequestObject = model.RequireRequestObject,
                AllowAccessTokensViaBrowser = model.AllowAccessTokensViaBrowser,
                FrontChannelLogoutUri = model.FrontChannelLogoutUri,
                FrontChannelLogoutSessionRequired = model.FrontChannelLogoutSessionRequired,
                BackChannelLogoutUri = model.BackChannelLogoutUri,
                BackChannelLogoutSessionRequired = model.BackChannelLogoutSessionRequired,
                AllowOfflineAccess = model.AllowOfflineAccess,
                IdentityTokenLifetime = model.IdentityTokenLifetime,
                AllowedIdentityTokenSigningAlgorithms = AllowedSigningAlgorithmsConverter.Convert(model.AllowedIdentityTokenSigningAlgorithms),
                AccessTokenLifetime = model.AccessTokenLifetime,
                AuthorizationCodeLifetime = model.AuthorizationCodeLifetime,
                ConsentLifetime = model.ConsentLifetime,
                AbsoluteRefreshTokenLifetime = model.AbsoluteRefreshTokenLifetime,
                SlidingRefreshTokenLifetime = model.SlidingRefreshTokenLifetime,
                RefreshTokenUsage = (int)model.RefreshTokenUsage,
                UpdateAccessTokenClaimsOnRefresh = model.UpdateAccessTokenClaimsOnRefresh,
                RefreshTokenExpiration = (int)model.RefreshTokenExpiration,
                AccessTokenType = (int)model.AccessTokenType,
                EnableLocalLogin = model.EnableLocalLogin,
                IncludeJwtId = model.IncludeJwtId,
                AlwaysSendClientClaims = model.AlwaysSendClientClaims,
                ClientClaimsPrefix = model.ClientClaimsPrefix,
                PairWiseSubjectSalt = model.PairWiseSubjectSalt,
                UserSsoLifetime = model.UserSsoLifetime,
                UserCodeType = model.UserCodeType,
                DeviceCodeLifetime = model.DeviceCodeLifetime,
                ProtocolType = model.ProtocolType,
                AllowedGrantTypes = model.AllowedGrantTypes?.Select(g => new Entities.ClientGrantType { GrantType = g }).ToList() ?? new List<Entities.ClientGrantType>(),
                RedirectUris = model.RedirectUris?.Select(r => new Entities.ClientRedirectUri { RedirectUri = r }).ToList() ?? new List<Entities.ClientRedirectUri>(),
                PostLogoutRedirectUris = model.PostLogoutRedirectUris?.Select(r => new Entities.ClientPostLogoutRedirectUri { PostLogoutRedirectUri = r }).ToList() ?? new List<Entities.ClientPostLogoutRedirectUri>(),
                AllowedScopes = model.AllowedScopes?.Select(s => new Entities.ClientScope { Scope = s }).ToList() ?? new List<Entities.ClientScope>(),
                IdentityProviderRestrictions = model.IdentityProviderRestrictions?.Select(r => new Entities.ClientIdPRestriction { Provider = r }).ToList() ?? new List<Entities.ClientIdPRestriction>(),
                AllowedCorsOrigins = model.AllowedCorsOrigins?.Select(o => new Entities.ClientCorsOrigin { Origin = o }).ToList() ?? new List<Entities.ClientCorsOrigin>(),
                Claims = model.Claims?.Select(c => new Entities.ClientClaim { Type = c.Type, Value = c.Value }).ToList() ?? new List<Entities.ClientClaim>(),
                Properties = model.Properties?.Select(p => new Entities.ClientProperty { Key = p.Key, Value = p.Value }).ToList() ?? new List<Entities.ClientProperty>(),
                ClientSecrets = model.ClientSecrets?.Select(s => new Entities.ClientSecret
                {
                    Description = s.Description,
                    Value = s.Value,
                    Expiration = s.Expiration,
                    Type = s.Type,
                }).ToList() ?? new List<Entities.ClientSecret>(),
            };

            return entity;
        }
    }
}
