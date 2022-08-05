import requests


with open('./Assets/StreamingAssets/DW06', 'rb') as f:
    r = requests.post('https://firebasestorage.googleapis.com/v0/b/condominium-assetbundles.appspot.com/o?uploadType=media&name=dw06', files={'DW06': f})
    print(r)
