using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace DataCore.Components
{
    public partial class FileComponent: Component
    {    
        public FileComponent()
        {
            InitializeComponent();
        }

        public FileComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
