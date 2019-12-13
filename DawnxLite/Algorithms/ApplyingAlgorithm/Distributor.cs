using NStandard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx.Algorithms.ApplyingAlgorithm
{
    public class Distributor
    {
        public class ProducerRest<TProducer>
            where TProducer : class
        {
            public TProducer Producer { get; set; }
            public int Rest { get; set; }

            public ProducerRest(TProducer producer, int rest)
            {
                Producer = producer;
                Rest = rest;
            }
        }

        public DistributionMethod Method { get; }

        public Distributor(DistributionMethod method)
        {
            Method = method;
        }

        public DistributionResult<TProducer, TConsumer>[] Distribute<TProducer, TConsumer>(DistributionContract<TProducer, TConsumer> contract)
            where TProducer : class
            where TConsumer : class
        {
            return Distribute(contract, out _);
        }

        public DistributionResult<TProducer, TConsumer>[] Distribute<TProducer, TConsumer>(DistributionContract<TProducer, TConsumer> contract, out ProducerRest<TProducer>[] rests)
            where TProducer : class
            where TConsumer : class
        {
            var results = new List<DistributionResult<TProducer, TConsumer>>();
            var _rests = contract.Producers.Select(x => new ProducerRest<TProducer>(x, contract.ProducerAmount(x))).ToArray();

            switch (Method)
            {
                case DistributionMethod.FormerPreferred:
                    var enumerator = _rests.GetEnumerator();
                    var rest = enumerator.TakeElement() as ProducerRest<TProducer>;

                    if (rest != null)
                    {
                        foreach (var consumer in contract.Consumers)
                        {
                            var capacity = contract.ConsumerAmount(consumer);
                            for (var over = false; !over && rest != null;)
                            {
                                if (rest.Rest >= capacity)
                                {
                                    var result = new DistributionResult<TProducer, TConsumer>(rest.Producer, consumer, capacity);
                                    results.Add(result);
                                    rest.Rest -= capacity;
                                    over = true;

                                    if (rest.Rest == 0)
                                        rest = enumerator.TakeElement() as ProducerRest<TProducer>;
                                }
                                else
                                {
                                    var result = new DistributionResult<TProducer, TConsumer>(rest.Producer, consumer, rest.Rest);
                                    results.Add(result);
                                    capacity -= rest.Rest;
                                    rest.Rest = 0;
                                    rest = enumerator.TakeElement() as ProducerRest<TProducer>;
                                }
                            }
                        }
                    }
                    break;
            }

            rests = _rests;
            return results.ToArray();
        }

    }

}
