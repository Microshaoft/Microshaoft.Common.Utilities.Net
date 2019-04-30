﻿#if NETCOREAPP2_X
namespace Microshaoft
{
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SyncAsyncActionSelector : IActionSelector
    {
        private ActionSelector _actionSelector;
        private readonly IConfiguration _configuration;
        public int InterceptCandidatesCount = 1;
        public string[] FilterControllerNamePrefixs;

        public SyncAsyncActionSelector
                    (
                        IActionDescriptorCollectionProvider actionDescriptorCollectionProvider
                        , ActionConstraintCache actionConstraintCache
                        , ILoggerFactory loggerFactory
                        , IConfiguration configuration
                    )
        {
            _actionSelector = new ActionSelector
                                        (
                                            actionDescriptorCollectionProvider
                                            , actionConstraintCache
                                            , loggerFactory
                                        );
            _configuration = configuration;
        }

        public
            Func
                <
                    RouteContext
                    , IReadOnlyList<ActionDescriptor>
                    , IConfiguration
                    , IReadOnlyList<ActionDescriptor>
                >
                    OnSelectSyncAsyncActionCandidate = null;


        public ActionDescriptor SelectBestCandidate
                                        (
                                            RouteContext context
                                            , IReadOnlyList<ActionDescriptor> candidates
                                        )
        {
            if (OnSelectSyncAsyncActionCandidate != null)
            {
                if (candidates.Count > InterceptCandidatesCount)
                {
                    var r = candidates
                                .Any
                                    (
                                        (x) =>
                                        {
                                            return
                                                FilterControllerNamePrefixs
                                                    .Any
                                                        (
                                                            (xx) =>
                                                            {
                                                                return
                                                                    ((ControllerActionDescriptor)x)
                                                                        .ControllerName
                                                                        .StartsWith
                                                                            (
                                                                                xx
                                                                                , StringComparison
                                                                                        .OrdinalIgnoreCase
                                                                            );
                                                            }
                                                        );
                                        }
                                    );
                    if (r)
                    {
                        candidates = OnSelectSyncAsyncActionCandidate
                                        (
                                            context
                                            , candidates
                                            , _configuration
                                        );
                    }
                }
            }
            return
                _actionSelector
                    .SelectBestCandidate
                        (
                            context
                            , candidates
                        );
        }

        public IReadOnlyList<ActionDescriptor> SelectCandidates(RouteContext context)
        {
            return
                _actionSelector
                    .SelectCandidates
                        (
                            context
                        );
        }
    }
}
#endif