namespace FA;

public delegate object FaValueGetAccessHandler(object? target);

public delegate void FaValueSetAccessHandler(object? target, object? value);

public delegate object FaInvokeAccessHandler(object? target, object[]? arguments);
