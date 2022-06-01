# PoolParty ☀️ 
Minimal C# Unity object pooling tool for easy implementation.

## How to Use
1. Add the Pool Party GameObject Pool script to adjacent to a spawner component. Instead of instantiating objects from the spawner, use PoolParty.Get() to grab one from the pool. 

2. Serialize your prefab to spawn in the inspector and configure the extra settings as preferred.

3. Each object created will have a ReturnToPool component added for keeping its reference to its pool. When your object is ready to be destroyed, use ReturnToPool.Release() and it will return to the pool.
