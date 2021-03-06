//-------------------------------------------------------------------------------
// <copyright file="EventScopingStandardFactory.cs" company="Appccelerate">
//   Copyright (c) 2008-2015
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Appccelerate.ScopingEventBroker
{
    using System;
    using Appccelerate.EventBroker;
    using Appccelerate.EventBroker.Factories;
    using Appccelerate.ScopingEventBroker.Internals.Context;

    public class EventScopingStandardFactory : StandardFactory, IEventScopeContextFactory
    {
        private readonly IEventScopeFactory eventScopeFactory;

        public EventScopingStandardFactory()
            : this(new PerCallEventScopeFactory())
        {
        }

        public EventScopingStandardFactory(IEventScopeFactory eventScopeFactory)
        {
            this.eventScopeFactory = eventScopeFactory;
        }

        public IEventScopeContext CreateScopeContext()
        {
            return this.eventScopeFactory.CreateScopeContext();
        }

        protected override IHandler ActivateHandler(Type handlerType)
        {
            IHandler handler = base.ActivateHandler(handlerType);

            return handler.Kind == HandlerKind.Asynchronous ? this.eventScopeFactory.CreateHandlerDecorator(handler) : handler;
        }
    }
}