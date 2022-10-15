using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal sealed class PackageManagerViewKoganePackageOnGitHubExtension
        : VisualElement,
          IPackageManagerExtension
    {
        private Button      m_button;
        private PackageInfo m_selectedPackageInfo;

        static PackageManagerViewKoganePackageOnGitHubExtension()
        {
            var extension = new PackageManagerViewKoganePackageOnGitHubExtension();
            PackageManagerExtensions.RegisterExtension( extension );
        }

        VisualElement IPackageManagerExtension.CreateExtensionUI()
        {
            m_button = new
            (
                () =>
                {
                    var url = $"https://github.com/baba-s/{m_selectedPackageInfo.displayName}";
                    url = url.Replace( "Kogane ", "Kogane." );
                    url = url.Replace( " ", "" );
                    Application.OpenURL( url );
                }
            )
            {
                text = "View on GitHub",
            };

            return m_button;
        }

        void IPackageManagerExtension.OnPackageSelectionChange( PackageInfo packageInfo )
        {
            m_selectedPackageInfo = packageInfo;
            m_button.visible      = packageInfo is { author: { name: "baba-s" } };
        }

        void IPackageManagerExtension.OnPackageAddedOrUpdated( PackageInfo packageInfo )
        {
        }

        void IPackageManagerExtension.OnPackageRemoved( PackageInfo packageInfo )
        {
        }
    }
}