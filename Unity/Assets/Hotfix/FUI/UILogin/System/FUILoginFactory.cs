using System.Threading.Tasks;
using ETModel;
using FairyGUI;
using UnityEngine;

namespace ETHotfix
{
    [FUIFactory(FUIType.FUILogin)]
    public class FUILoginFactory : IFUIFactory
    {
        public async ETTask<FUI> Create(string type)
        {
	        await Task.CompletedTask;
	        
	        // 可以同步或者异步加载,异步加载需要搞个转圈圈,这里为了简单使用同步加载
	        ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
	        resourcesComponent.LoadBundle(type.StringToAB());
	        
	        AssetBundle assetBundle = resourcesComponent.GetAssetBundle(type.StringToAB());
	        UIPackage.AddPackage(assetBundle);
	        FUI fui = ComponentFactory.Create<FUI, GObject>(UIPackage.CreateObject("Login", "LoginComponent"));
	        fui.AddComponent<FUILoginComponent>();
	        
	        return fui;
        }

	    public void Remove(string type)
	    {
	    }
    }
}