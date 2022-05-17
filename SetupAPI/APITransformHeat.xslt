<xsl:stylesheet version="1.0"
            xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
            xmlns:msxsl="urn:schemas-microsoft-com:xslt"
            exclude-result-prefixes="msxsl"
            xmlns:wix="http://schemas.microsoft.com/wix/2006/wi"
            xmlns:my="my:my">

  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>

  <!-- Remove o .exe para incluir atalhos direto pelo wxs principal. -->
  <!-- Para "achar" extensões usar: <xsl:key name="test" match="wix:Component[contains(wix:File/@Source, '.test')]" use="@Id"/> -->
  <xsl:key name="exe-search" match="wix:Component[wix:File/@Source = '$(var.DirPacote)\APIluminacao.exe']" use="@Id"/>

  <xsl:template match="wix:Component[key('exe-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('exe-search', @Id)]"/>

</xsl:stylesheet>