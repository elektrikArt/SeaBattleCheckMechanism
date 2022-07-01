
Get-ChildItem -Exclude "*.a","*.mine" ./tests/ | ForEach-Object {
    $right = Get-Content -Path ("./tests/" + $_.BaseName + ".a")
    $result = Get-Content -Path ("./tests/" + $_.BaseName + ".mine")

    #"input_:"
    #$input_
    #"right:"
    #$right
    #"result:"
    #$result

    "./tests/" + $_.BaseName + ":"
    Compare-Object -ReferenceObject $right -DifferenceObject $result

    #$result | Tee-Object -FilePath ("./tests/" + $_.BaseName + ".mine") | Out-Null

    #Read-Host
}