using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using System.Text;
namespace FanLiHang.Test
{
    class Program
    {
        static IDatabase database;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("47.104.4.117:6379");
            database = connectionMultiplexer.GetDatabase();

            Parallel.Invoke(
                DesrList);

            Console.ReadKey();
        }

        static void DesrList()
        {
            StringBuilder sb = GetDesrListSql();
            try
            {
                var result = database.ScriptEvaluate(sb.ToString(), new RedisKey[] { "7770770608", "7770770508" }, new RedisValue[] { 1, 10 });
                Console.WriteLine(result.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static StringBuilder GetDesrListSql()
        {
            return new StringBuilder(@"
                local stocks = {}
                stocks[1] = '1'
                for i=1,#KEYS
                do
                    local qty = redis.call('llen',KEYS[i])
                    if(qty<tonumber(ARGV[i]))
                    then
                        stocks[1] = '0'
                    end
                    stocks[#stocks+1] = KEYS[i] .. ':' .. qty .. '|' .. ARGV[i] 
                end
                if(stocks[1]==1)
                then
                     stocks={}
                    stocks[1] = 1
                    for i=1,#KEYS
                    do
                        for j=1,tonumber(ARGV[i])
                        do
                            stocks[#stocks+1] = redis.call('rpop',KEYS[i])
                        end
                    end
                else
                    return stocks
                end
                
            ");
        }

        static void Desr()
        {
            StringBuilder sb = new StringBuilder(@"
                    for i=1,#KEYS 
                    do
                       if (redis.call('decrby', KEYS[i], ARGV[i]) < 0)
                            then
                            for j = 1, i
                            do
                                redis.call('incrby', KEYS[j], ARGV[j])
                            end
                            return 1
                        end
                    end
                    return 0");
            try
            {
                database.ScriptEvaluate(sb.ToString(), new RedisKey[] { "77707706", "77707705" }, new RedisValue[] { 1, 1 });
            }
            catch
            {

            }
        }
    }
}
