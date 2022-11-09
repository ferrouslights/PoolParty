# PoolParty ☀️ 
Minimal C# Unity object pooling tool for easy implementation on spawned GameObjects.

## How to Use
1. Add a component inheriting PoolPartyReleaserBase (PoolParty is a simple implementation) to your spawner and serialize your prefab. Instead of instantiating objects from the spawner, use the components .Get() to grab one from the pool. 

2. Each object created will have a PoolPartyReleaserBase component added for keeping its reference to its pool. When your object is ready to be destroyed, use its .Release() method. You can also subscribe to its OnAddComponent and OnRelease events for extra behavior.

Enjoy the pool! 🏊‍♀️ 
