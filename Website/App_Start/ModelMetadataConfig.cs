//------------------------------------------------------------------------------------------------- 
// <copyright file="ModelMetaConfig.cs" company="inxin bvba">
// Copyright 2014-2015 inxin bvba.
// 
// Dual Licensed under
//   a) the Affero General Public Licence v3 (AGPL)
//   b) the Allors License
// 
// The AGPL License is included in the file LICENSE.
// The Allors License is an addendum to your contract.
// 
// Dipu is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// For more information visit http://www.dipu.com/legal
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Website
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    using Allors.Meta;
    using Allors.Web.Mvc;
    using Allors.Web.Mvc.Models;

    public class ModelMetadataConfig
    {
        public static AllorsModelMetadataProvider ModelMetadataProvider
        {
            get
            {
                return new AllorsModelMetadataProvider(CompositeByModelType)
                           {
                               MetadataFilters =
                                   {
                                       new IdFilter(),
                                       new LabelFilter(),
                                       new WatermarkFilter(),
                                    }
                           };
            }
        }

        public static Dictionary<Type, Composite> CompositeByModelType
        {
            get
            {
                var modelTypes =
                    Assembly.GetExecutingAssembly()
                        .GetTypes()
                        .Where(type => 
                            type != typeof(ICompositeModel) && 
                            type != typeof(CompositeModel<>) && 
                            typeof(ICompositeModel).IsAssignableFrom(type))
                        .ToArray();

                var compositeByModelType = new Dictionary<Type, Composite>();
                foreach (var modelType in modelTypes)
                {
                    var genericInterface = GetModelGenericInterfaceMarker(modelType);
                    if (genericInterface != null)
                    {
                        var compositeType = genericInterface.GetGenericArguments()[0];
                        var property = compositeType.GetProperty("Instance");
                        var composite = (Composite)property.GetMethod.Invoke(null, null);
                        compositeByModelType[modelType] = composite;
                    }
                    else
                    {
                        compositeByModelType[modelType] = null;
                    }
                }

                return compositeByModelType;
            }
        }

        public static void Register()
        {
            ModelMetadataProviders.Current = ModelMetadataProvider;
        }

        private static Type GetModelGenericInterfaceMarker(Type type)
        {
            while (type != null)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(CompositeModel<>))
                {
                    return type;
                }

                type = type.BaseType;
            }

            return null;
        }
    }
}
