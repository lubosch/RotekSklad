namespace GemBox.Spreadsheet
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Line styles used for 
    /// <see cref="P:GemBox.Spreadsheet.CellBorder.LineStyle">CellBorder.LineStyle</see>.
    /// </summary>
    public enum LineStyle
    {
        /// <summary>
        /// Dash-dot line.
        /// </summary>
        DashDot = 9,
        /// <summary>
        /// Dash-dot-dot line.
        /// </summary>
        DashDotDot = 11,
        /// <summary>
        /// Dashed line.
        /// </summary>
        Dashed = 3,
        /// <summary>
        /// Dotted line.
        /// </summary>
        Dotted = 4,
        /// <summary>
        /// Double line.
        /// </summary>
        Double = 6,
        /// <summary>
        /// Obsolete. Use Double instead.
        /// </summary>
        /// <exclude />
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use Double instead.")]
        DoubleLine = 6,
        /// <summary>
        /// Hair line.
        /// </summary>
        Hair = 7,
        /// <summary>
        /// Medium line.
        /// </summary>
        Medium = 2,
        /// <summary>
        /// Medium dash-dot line.
        /// </summary>
        MediumDashDot = 10,
        /// <summary>
        /// Medium dash-dot-dot line.
        /// </summary>
        MediumDashDotDot = 12,
        /// <summary>
        /// Medium dashed line.
        /// </summary>
        MediumDashed = 8,
        /// <summary>
        /// No line.
        /// </summary>
        None = 0,
        /// <summary>
        /// Slanted dash-dot line.
        /// </summary>
        SlantDashDot = 13,
        /// <summary>
        /// Obsolete. Use SlantDashDot instead.
        /// </summary>
        /// <exclude />
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use SlantDashDot instead.")]
        SlantedDashDot = 13,
        /// <summary>
        /// Thick line.
        /// </summary>
        Thick = 5,
        /// <summary>
        /// Thin line.
        /// </summary>
        Thin = 1
    }
}

