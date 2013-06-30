<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.1">
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	
	<xsl:template match="resultset">
		<markers><xsl:apply-templates/></markers>		 
	</xsl:template>

	<xsl:template match="row">
		<xsl:element name="marker">
			<xsl:for-each select="field">
				<xsl:attribute name="{@name}"><xsl:value-of select="."/></xsl:attribute>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>
</xsl:stylesheet>
