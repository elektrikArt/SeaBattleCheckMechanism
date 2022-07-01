
Get-ChildItem -Exclude "*.a","*.mine" ./tests/ | ForEach-Object {
    #$right = Get-Content ("./tests/" + $_.BaseName + ".a")

    $input_ = Get-Content $_
    $result = $input_ | .\bin\Debug\net6.0\H_G_obrazniy_morskoy_boy.exe

    "input_:"
    $input_
    #"right:"
    #$right
    #"result:"
    #$result

    #Compare-Object -ReferenceObject $right -DifferenceObject $result -IncludeEqual

    $result | Tee-Object -FilePath ("./tests/" + $_.BaseName + ".mine") | Out-Null

    #Read-Host
}