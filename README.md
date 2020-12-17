## I was bored

### BetterHints

Better hints is a hint queueing and priority system for the SCP:Secret Labs [exiled](https://github.com/galaxy119/EXILED) plugin framework.

The latest stable release can be found [here](https://github.com/steven4547466/BetterHints/releases/latest).

# IMPORTANT INFO

BetterHints is a **PLUGIN** not a dependency, though you use it as one. If you use BetterHints, you should tell the server owner to put BetterHints in their plugin folder for it to work. The server owner will only ever need one instance of the plugin running for it to work across any number of plugins.

# Accessing the API

I recommend reflection to access the API. You should have a method that will access the api for you so you can easily queue hints.

Something as simple as

```cs
public static void QueueHint(Player player, string message, float duration = 3f, int priority = 0, bool disableOnDeath = false, bool overrideHint = false) {
  Assembly assembly = Loader.Plugins.FirstOrDefault(pl => pl.Name == "BetterHints")?.Assembly;
  if (assembly == null) return;
  assembly?.GetType("BetterHints.API")?.GetMethod("QueueHint")?.Invoke(null, new[] { player, message, duration, priority, disableOnDeath, overrideHint });
}
```

This is broken up a little for readability, but you can one-line it with this code:

```cs
public static void QueueHint(Player player, string message, float duration = 3f, int priority = 0, bool disableOnDeath = false, bool overrideHint = false) {
  Loader.Plugins.FirstOrDefault(pl => pl.Name == "BetterHints")?.Assembly?.GetType("BetterHints.API")?.GetMethod("QueueHint")?.Invoke(null, new[] { player, message, duration, priority, disableOnDeath, overrideHint });
}
```