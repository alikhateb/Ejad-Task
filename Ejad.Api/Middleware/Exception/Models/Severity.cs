﻿namespace Ejad.Api.Middleware.Exception.Models;

/// <summary>
///     the exception severity
/// </summary>
public enum Severity
{
    /// <summary>
    ///     Undefined
    /// </summary>
    Undefined,

    /// <summary>
    ///     Warning
    /// </summary>
    Warning,

    /// <summary>
    ///     Information
    /// </summary>
    Information,

    /// <summary>
    ///     Critical
    /// </summary>
    Critical
}