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

To use it, you would simply call the `QueueHint` method from your code with the required parameters.

However, should you choose to reference the plugin, you can use the `BetterHints.API.QueueHint` method. Its parameters are the same as the methods above, along with the defaults. There is also a `Player.QueueHint` extension, should you choose to reference the plugin, that you could use to skip the API method call.

## Expectations

Here's some things you can expect while using this plugin:

If a plugin uses the regular hint system given by exiled it will **always** override whatever hint is currently showing to keep compatibility with the base game.

`overrideHint` will only override the currently shown hint if the priority of the new hint is greater than or eqaul to the priority of the current hint.

`overrideHint` puts the hint to the front of the queue for equal priority hints if it doesn't override the current hint.

The max `priority` is `int.MaxValue` (go figure) with the min value being `int.MinValue` (again go figure).

A hint with the priority `int.MaxValue` will always go to the front of the queue and could override the current hint if enabled. While a hint with the priority `int.MinValue` will always go to the end of the queue and will not override any other hint positions.

Hints will always go to the end of the queue of the same priority hints unless `overrideHint` is enabled.