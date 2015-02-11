﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HybridDb.Studio.Views
{
    public class TabContentSelector : DataTemplateSelector
    {
        public TabContentSelector()
        {
            DataTemplates = new Dictionary<string, DataTemplate>();
        }

        public Dictionary<string, DataTemplate> DataTemplates { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return DataTemplates[item.GetType().Name];
        }
    }
}