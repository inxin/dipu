// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlStrictValidation.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors
{
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;

    public class XmlStrictValidation
    {
        #region Xml Strict Schema
        private const string SchemaString = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<xs:schema version=""1.0"" xml:lang=""en""
    xmlns:xs=""http://www.w3.org/2001/XMLSchema""
    targetNamespace=""http://www.w3.org/1999/xhtml""
    xmlns=""http://www.w3.org/1999/xhtml""
    xmlns:xml=""http://www.w3.org/XML/1998/namespace""
    elementFormDefault=""qualified"">

  <xs:annotation>
    <xs:documentation>
    XHTML 1.0 (Second Edition) Strict in XML Schema

    This is the same as HTML 4 Strict except for
    changes due to the differences between XML and SGML.

    Namespace = http://www.w3.org/1999/xhtml

    For further information, see: http://www.w3.org/TR/xhtml1

    Copyright (c) 1998-2002 W3C (MIT, INRIA, Keio),
    All Rights Reserved. 

    The DTD version is identified by the PUBLIC and SYSTEM identifiers:

    PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN""
    SYSTEM ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd""

    $Id: xhtml1-strict.xsd,v 1.2 2002/08/28 08:05:44 mimasa Exp $
    </xs:documentation>
  </xs:annotation>

  <xs:import namespace=""http://www.w3.org/XML/1998/namespace""
      schemaLocation=""http://www.w3.org/2001/xml.xsd""/>

  <xs:annotation>
    <xs:documentation>
    ================ Character mnemonic entities =========================

    XHTML entity sets are identified by the PUBLIC and SYSTEM identifiers:
  
    PUBLIC ""-//W3C//ENTITIES Latin 1 for XHTML//EN""
    SYSTEM ""http://www.w3.org/TR/xhtml1/DTD/xhtml-lat1.ent""

    PUBLIC ""-//W3C//ENTITIES Special for XHTML//EN""
    SYSTEM ""http://www.w3.org/TR/xhtml1/DTD/xhtml-special.ent""

    PUBLIC ""-//W3C//ENTITIES Symbols for XHTML//EN""
    SYSTEM ""http://www.w3.org/TR/xhtml1/DTD/xhtml-symbol.ent""
    </xs:documentation>
  </xs:annotation>

  <xs:annotation>
    <xs:documentation>
    ================== Imported Names ====================================
    </xs:documentation>
  </xs:annotation>

  <xs:simpleType name=""ContentType"">
    <xs:annotation>
      <xs:documentation>
      media type, as per [RFC2045]
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:string""/>
  </xs:simpleType>

  <xs:simpleType name=""ContentTypes"">
    <xs:annotation>
      <xs:documentation>
      comma-separated list of media types, as per [RFC2045]
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:string""/>
  </xs:simpleType>

  <xs:simpleType name=""Charset"">
    <xs:annotation>
      <xs:documentation>
      a character encoding, as per [RFC2045]
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:string""/>
  </xs:simpleType>

  <xs:simpleType name=""Charsets"">
    <xs:annotation>
      <xs:documentation>
      a space separated list of character encodings, as per [RFC2045]
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:string""/>
  </xs:simpleType>

  <xs:simpleType name=""LanguageCode"">
    <xs:annotation>
      <xs:documentation>
      a language code, as per [RFC3066]
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:language""/>
  </xs:simpleType>

  <xs:simpleType name=""Character"">
    <xs:annotation>
      <xs:documentation>
      a single character, as per section 2.2 of [XML]
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:string"">
      <xs:length value=""1"" fixed=""true""/>
    </xs:restriction>
  </xs:simpleType>

  <xs:simpleType name=""Number"">
    <xs:annotation>
      <xs:documentation>
      one or more digits
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:nonNegativeInteger"">
      <xs:pattern value=""[0-9]+""/>
    </xs:restriction>
  </xs:simpleType>

  <xs:simpleType name=""tabindexNumber"">
    <xs:annotation>
      <xs:documentation>
      tabindex attribute specifies the position of the current element
      in the tabbing order for the current document. This value must be
      a number between 0 and 32767. User agents should ignore leading zeros. 
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""Number"">
      <xs:minInclusive value=""0""/>
      <xs:maxInclusive value=""32767""/>
    </xs:restriction>
  </xs:simpleType>

  <xs:simpleType name=""LinkTypes"">
    <xs:annotation>
      <xs:documentation>
      space-separated list of link types
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:NMTOKENS""/>
  </xs:simpleType>

  <xs:simpleType name=""MediaDesc"">
    <xs:annotation>
      <xs:documentation>
      single or comma-separated list of media descriptors
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:string"">
      <xs:pattern value=""[^,]+(,\s*[^,]+)*""/>
    </xs:restriction>
  </xs:simpleType>

  <xs:simpleType name=""URI"">
    <xs:annotation>
      <xs:documentation>
      a Uniform Resource Identifier, see [RFC2396]
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:anyURI""/>
  </xs:simpleType>

  <xs:simpleType name=""UriList"">
    <xs:annotation>
      <xs:documentation>
      a space separated list of Uniform Resource Identifiers
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:string""/>
  </xs:simpleType>

  <xs:simpleType name=""Datetime"">
    <xs:annotation>
      <xs:documentation>
      date and time information. ISO date format
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:dateTime""/>
  </xs:simpleType>

  <xs:simpleType name=""Script"">
    <xs:annotation>
      <xs:documentation>
      script expression
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:string""/>
  </xs:simpleType>

  <xs:simpleType name=""StyleSheet"">
    <xs:annotation>
      <xs:documentation>
      style sheet data
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:string""/>
  </xs:simpleType>

  <xs:simpleType name=""Text"">
    <xs:annotation>
      <xs:documentation>
      used for titles etc.
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:string""/>
  </xs:simpleType>

  <xs:simpleType name=""Length"">
    <xs:annotation>
      <xs:documentation>
      nn for pixels or nn% for percentage length
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:string"">
      <xs:pattern value=""[-+]?(\d+|\d+(\.\d+)?%)""/>
    </xs:restriction>
  </xs:simpleType>

  <xs:simpleType name=""MultiLength"">
    <xs:annotation>
      <xs:documentation>
      pixel, percentage, or relative
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:string"">
      <xs:pattern value=""[-+]?(\d+|\d+(\.\d+)?%)|[1-9]?(\d+)?\*""/>
    </xs:restriction>
  </xs:simpleType>

  <xs:simpleType name=""Pixels"">
    <xs:annotation>
      <xs:documentation>
      integer representing length in pixels
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:nonNegativeInteger""/>
  </xs:simpleType>

  <xs:annotation>
    <xs:documentation>
    these are used for image maps
    </xs:documentation>
  </xs:annotation>

  <xs:simpleType name=""Shape"">
    <xs:restriction base=""xs:token"">
      <xs:enumeration value=""rect""/>
      <xs:enumeration value=""circle""/>
      <xs:enumeration value=""poly""/>
      <xs:enumeration value=""default""/>
    </xs:restriction>
  </xs:simpleType>

  <xs:simpleType name=""Coords"">
    <xs:annotation>
      <xs:documentation>
      comma separated list of lengths
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:string"">
      <xs:pattern
          value=""[-+]?(\d+|\d+(\.\d+)?%)(,\s*[-+]?(\d+|\d+(\.\d+)?%))*""/>
    </xs:restriction>
  </xs:simpleType>

  <xs:annotation>
    <xs:documentation>
    =================== Generic Attributes ===============================
    </xs:documentation>
  </xs:annotation>

  <xs:attributeGroup name=""coreattrs"">
    <xs:annotation>
      <xs:documentation>
      core attributes common to most elements
      id       document-wide unique id
      class    space separated list of classes
      style    associated style info
      title    advisory title/amplification
      </xs:documentation>
    </xs:annotation>
    <xs:attribute name=""id"" type=""xs:ID""/>
    <xs:attribute name=""class"" type=""xs:NMTOKENS""/>
    <xs:attribute name=""style"" type=""StyleSheet""/>
    <xs:attribute name=""title"" type=""Text""/>
  </xs:attributeGroup>

  <xs:attributeGroup name=""i18n"">
    <xs:annotation>
      <xs:documentation>
      internationalization attributes
      lang        language code (backwards compatible)
      xml:lang    language code (as per XML 1.0 spec)
      dir         direction for weak/neutral text
      </xs:documentation>
    </xs:annotation>
    <xs:attribute name=""lang"" type=""LanguageCode""/>
    <xs:attribute ref=""xml:lang""/>
    <xs:attribute name=""dir"">
      <xs:simpleType>
        <xs:restriction base=""xs:token"">
          <xs:enumeration value=""ltr""/>
          <xs:enumeration value=""rtl""/>
        </xs:restriction>
      </xs:simpleType>
    </xs:attribute>
  </xs:attributeGroup>

  <xs:attributeGroup name=""events"">
    <xs:annotation>
      <xs:documentation>
      attributes for common UI events
      onclick     a pointer button was clicked
      ondblclick  a pointer button was double clicked
      onmousedown a pointer button was pressed down
      onmouseup   a pointer button was released
      onmousemove a pointer was moved onto the element
      onmouseout  a pointer was moved away from the element
      onkeypress  a key was pressed and released
      onkeydown   a key was pressed down
      onkeyup     a key was released
      </xs:documentation>
    </xs:annotation>
    <xs:attribute name=""onclick"" type=""Script""/>
    <xs:attribute name=""ondblclick"" type=""Script""/>
    <xs:attribute name=""onmousedown"" type=""Script""/>
    <xs:attribute name=""onmouseup"" type=""Script""/>
    <xs:attribute name=""onmouseover"" type=""Script""/>
    <xs:attribute name=""onmousemove"" type=""Script""/>
    <xs:attribute name=""onmouseout"" type=""Script""/>
    <xs:attribute name=""onkeypress"" type=""Script""/>
    <xs:attribute name=""onkeydown"" type=""Script""/>
    <xs:attribute name=""onkeyup"" type=""Script""/>
  </xs:attributeGroup>

  <xs:attributeGroup name=""focus"">
    <xs:annotation>
      <xs:documentation>
      attributes for elements that can get the focus
      accesskey   accessibility key character
      tabindex    position in tabbing order
      onfocus     the element got the focus
      onblur      the element lost the focus
      </xs:documentation>
    </xs:annotation>
    <xs:attribute name=""accesskey"" type=""Character""/>
    <xs:attribute name=""tabindex"" type=""tabindexNumber""/>
    <xs:attribute name=""onfocus"" type=""Script""/>
    <xs:attribute name=""onblur"" type=""Script""/>
  </xs:attributeGroup>

  <xs:attributeGroup name=""attrs"">
    <xs:attributeGroup ref=""coreattrs""/>
    <xs:attributeGroup ref=""i18n""/>
    <xs:attributeGroup ref=""events""/>
  </xs:attributeGroup>

  <xs:annotation>
    <xs:documentation>
    =================== Text Elements ====================================
    </xs:documentation>
  </xs:annotation>

  <xs:group name=""special.pre"">
    <xs:choice>
      <xs:element ref=""br""/>
      <xs:element ref=""span""/>
      <xs:element ref=""bdo""/>
      <xs:element ref=""map""/>
    </xs:choice>
  </xs:group>

  <xs:group name=""special"">
    <xs:choice>
      <xs:group ref=""special.pre""/>
      <xs:element ref=""object""/>
      <xs:element ref=""img""/>
    </xs:choice>
  </xs:group>

  <xs:group name=""fontstyle"">
    <xs:choice>
      <xs:element ref=""tt""/>
      <xs:element ref=""i""/>
      <xs:element ref=""b""/>
      <xs:element ref=""big""/>
      <xs:element ref=""small""/>
    </xs:choice>
  </xs:group>

  <xs:group name=""phrase"">
    <xs:choice>
      <xs:element ref=""em""/>
      <xs:element ref=""strong""/>
      <xs:element ref=""dfn""/>
      <xs:element ref=""code""/>
      <xs:element ref=""q""/>
      <xs:element ref=""samp""/>
      <xs:element ref=""kbd""/>
      <xs:element ref=""var""/>
      <xs:element ref=""cite""/>
      <xs:element ref=""abbr""/>
      <xs:element ref=""acronym""/>
      <xs:element ref=""sub""/>
      <xs:element ref=""sup""/>
    </xs:choice>
  </xs:group>

  <xs:group name=""inline.forms"">
    <xs:choice>
      <xs:element ref=""input""/>
      <xs:element ref=""select""/>
      <xs:element ref=""textarea""/>
      <xs:element ref=""label""/>
      <xs:element ref=""button""/>
    </xs:choice>
  </xs:group>

  <xs:group name=""misc.inline"">
    <xs:choice>
      <xs:element ref=""ins""/>
      <xs:element ref=""del""/>
      <xs:element ref=""script""/>
    </xs:choice>
  </xs:group>

  <xs:group name=""misc"">
    <xs:annotation>
      <xs:documentation>
      these can only occur at block level
      </xs:documentation>
    </xs:annotation>
    <xs:choice>
      <xs:element ref=""noscript""/>
      <xs:group ref=""misc.inline""/>
    </xs:choice>
  </xs:group>

  <xs:group name=""inline"">
    <xs:choice>
      <xs:element ref=""a""/>
      <xs:group ref=""special""/>
      <xs:group ref=""fontstyle""/>
      <xs:group ref=""phrase""/>
      <xs:group ref=""inline.forms""/>
    </xs:choice>
  </xs:group>

  <xs:complexType name=""Inline"" mixed=""true"">
    <xs:annotation>
      <xs:documentation>
      ""Inline"" covers inline or ""text-level"" elements
      </xs:documentation>
    </xs:annotation>
    <xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
      <xs:group ref=""inline""/>
      <xs:group ref=""misc.inline""/>
    </xs:choice>
  </xs:complexType>

  <xs:annotation>
    <xs:documentation>
    ================== Block level elements ==============================
    </xs:documentation>
  </xs:annotation>

  <xs:group name=""heading"">
    <xs:choice>
      <xs:element ref=""h1""/>
      <xs:element ref=""h2""/>
      <xs:element ref=""h3""/>
      <xs:element ref=""h4""/>
      <xs:element ref=""h5""/>
      <xs:element ref=""h6""/>
    </xs:choice>
  </xs:group>

  <xs:group name=""lists"">
    <xs:choice>
      <xs:element ref=""ul""/>
      <xs:element ref=""ol""/>
      <xs:element ref=""dl""/>
    </xs:choice>
  </xs:group>

  <xs:group name=""blocktext"">
    <xs:choice>
      <xs:element ref=""pre""/>
      <xs:element ref=""hr""/>
      <xs:element ref=""blockquote""/>
      <xs:element ref=""address""/>
    </xs:choice>
  </xs:group>

  <xs:group name=""block"">
    <xs:choice>
      <xs:element ref=""p""/>
      <xs:group ref=""heading""/>
      <xs:element ref=""div""/>
      <xs:group ref=""lists""/>
      <xs:group ref=""blocktext""/>
      <xs:element ref=""fieldset""/>
      <xs:element ref=""table""/>
    </xs:choice>
  </xs:group>

  <xs:complexType name=""Block"">
    <xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
      <xs:group ref=""block""/>
      <xs:element ref=""form""/>
      <xs:group ref=""misc""/>
    </xs:choice>
  </xs:complexType>

  <xs:complexType name=""Flow"" mixed=""true"">
    <xs:annotation>
      <xs:documentation>
      ""Flow"" mixes block and inline and is used for list items etc.
      </xs:documentation>
    </xs:annotation>
    <xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
      <xs:group ref=""block""/>
      <xs:element ref=""form""/>
      <xs:group ref=""inline""/>
      <xs:group ref=""misc""/>
    </xs:choice>
  </xs:complexType>

  <xs:annotation>
    <xs:documentation>
    ================== Content models for exclusions =====================
    </xs:documentation>
  </xs:annotation>

  <xs:complexType name=""a.content"" mixed=""true"">
    <xs:annotation>
      <xs:documentation>
      a elements use ""Inline"" excluding a
      </xs:documentation>
    </xs:annotation>
    <xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
      <xs:group ref=""special""/>
      <xs:group ref=""fontstyle""/>
      <xs:group ref=""phrase""/>
      <xs:group ref=""inline.forms""/>
      <xs:group ref=""misc.inline""/>
    </xs:choice>
  </xs:complexType>

  <xs:complexType name=""pre.content"" mixed=""true"">
    <xs:annotation>
      <xs:documentation>
      pre uses ""Inline"" excluding big, small, sup or sup
      </xs:documentation>
    </xs:annotation>
    <xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
      <xs:element ref=""a""/>
      <xs:group ref=""fontstyle""/>
      <xs:group ref=""phrase""/>
      <xs:group ref=""special.pre""/>
      <xs:group ref=""misc.inline""/>
      <xs:group ref=""inline.forms""/>
    </xs:choice>
  </xs:complexType>

  <xs:complexType name=""form.content"">
    <xs:annotation>
      <xs:documentation>
      form uses ""Block"" excluding form
      </xs:documentation>
    </xs:annotation>
    <xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
      <xs:group ref=""block""/>
      <xs:group ref=""misc""/>
    </xs:choice>
  </xs:complexType>

  <xs:complexType name=""button.content"" mixed=""true"">
    <xs:annotation>
      <xs:documentation>
      button uses ""Flow"" but excludes a, form and form controls
      </xs:documentation>
    </xs:annotation>
    <xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
      <xs:element ref=""p""/>
      <xs:group ref=""heading""/>
      <xs:element ref=""div""/>
      <xs:group ref=""lists""/>
      <xs:group ref=""blocktext""/>
      <xs:element ref=""table""/>
      <xs:group ref=""special""/>
      <xs:group ref=""fontstyle""/>
      <xs:group ref=""phrase""/>
      <xs:group ref=""misc""/>
    </xs:choice>
  </xs:complexType>

  <xs:annotation>
    <xs:documentation>
    ================ Document Structure ==================================
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""html"">
    <xs:complexType>
      <xs:sequence>
        <xs:element ref=""head""/>
        <xs:element ref=""body""/>
      </xs:sequence>
      <xs:attributeGroup ref=""i18n""/>
      <xs:attribute name=""id"" type=""xs:ID""/>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    ================ Document Head =======================================
    </xs:documentation>
  </xs:annotation>

  <xs:group name=""head.misc"">
    <xs:sequence>
      <xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
        <xs:element ref=""script""/>
        <xs:element ref=""style""/>
        <xs:element ref=""meta""/>
        <xs:element ref=""link""/>
        <xs:element ref=""object""/>
      </xs:choice>
    </xs:sequence>
  </xs:group>

  <xs:element name=""head"">
    <xs:annotation>
      <xs:documentation>
      content model is ""head.misc"" combined with a single
      title and an optional base element in any order
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:group ref=""head.misc""/>
        <xs:choice>
          <xs:sequence>
            <xs:element ref=""title""/>
            <xs:group ref=""head.misc""/>
            <xs:sequence minOccurs=""0"">
              <xs:element ref=""base""/>
              <xs:group ref=""head.misc""/>
            </xs:sequence>
          </xs:sequence>
          <xs:sequence>
            <xs:element ref=""base""/>
            <xs:group ref=""head.misc""/>
            <xs:element ref=""title""/>
            <xs:group ref=""head.misc""/>
          </xs:sequence>
        </xs:choice>
      </xs:sequence>
      <xs:attributeGroup ref=""i18n""/>
      <xs:attribute name=""id"" type=""xs:ID""/>
      <xs:attribute name=""profile"" type=""URI""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""title"">
    <xs:annotation>
      <xs:documentation>
      The title element is not considered part of the flow of text.
      It should be displayed, for example as the page header or
      window title. Exactly one title is required per document.
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:attributeGroup ref=""i18n""/>
      <xs:attribute name=""id"" type=""xs:ID""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""base"">
    <xs:annotation>
      <xs:documentation>
      document base URI
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:attribute name=""href"" use=""required"" type=""URI""/>
      <xs:attribute name=""id"" type=""xs:ID""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""meta"">
    <xs:annotation>
      <xs:documentation>
      generic metainformation
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:attributeGroup ref=""i18n""/>
      <xs:attribute name=""id"" type=""xs:ID""/>
      <xs:attribute name=""http-equiv""/>
      <xs:attribute name=""name""/>
      <xs:attribute name=""content"" use=""required""/>
      <xs:attribute name=""scheme""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""link"">
    <xs:annotation>
      <xs:documentation>
      Relationship values can be used in principle:

      a) for document specific toolbars/menus when used
         with the link element in document head e.g.
           start, contents, previous, next, index, end, help
      b) to link to a separate style sheet (rel=""stylesheet"")
      c) to make a link to a script (rel=""script"")
      d) by stylesheets to control how collections of
         html nodes are rendered into printed documents
      e) to make a link to a printable version of this document
         e.g. a PostScript or PDF version (rel=""alternate"" media=""print"")
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:attributeGroup ref=""attrs""/>
      <xs:attribute name=""charset"" type=""Charset""/>
      <xs:attribute name=""href"" type=""URI""/>
      <xs:attribute name=""hreflang"" type=""LanguageCode""/>
      <xs:attribute name=""type"" type=""ContentType""/>
      <xs:attribute name=""rel"" type=""LinkTypes""/>
      <xs:attribute name=""rev"" type=""LinkTypes""/>
      <xs:attribute name=""media"" type=""MediaDesc""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""style"">
    <xs:annotation>
      <xs:documentation>
      style info, which may include CDATA sections
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:attributeGroup ref=""i18n""/>
      <xs:attribute name=""id"" type=""xs:ID""/>
      <xs:attribute name=""type"" use=""required"" type=""ContentType""/>
      <xs:attribute name=""media"" type=""MediaDesc""/>
      <xs:attribute name=""title"" type=""Text""/>
      <xs:attribute ref=""xml:space"" fixed=""preserve""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""script"">
    <xs:annotation>
      <xs:documentation>
      script statements, which may include CDATA sections
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:attribute name=""id"" type=""xs:ID""/>
      <xs:attribute name=""charset"" type=""Charset""/>
      <xs:attribute name=""type"" use=""required"" type=""ContentType""/>
      <xs:attribute name=""src"" type=""URI""/>
      <xs:attribute name=""defer"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""defer""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute ref=""xml:space"" fixed=""preserve""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""noscript"">
    <xs:annotation>
      <xs:documentation>
      alternate content container for non script-based rendering
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:complexContent>
        <xs:extension base=""Block"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    =================== Document Body ====================================
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""body"">
    <xs:complexType>
      <xs:complexContent>
        <xs:extension base=""Block"">
          <xs:attributeGroup ref=""attrs""/>
          <xs:attribute name=""onload"" type=""Script""/>
          <xs:attribute name=""onunload"" type=""Script""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""div"">
    <xs:annotation>
      <xs:documentation>
      generic language/style container      
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Flow"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    =================== Paragraphs =======================================
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""p"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    =================== Headings =========================================

    There are six levels of headings from h1 (the most important)
    to h6 (the least important).
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""h1"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""h2"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""h3"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""h4"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""h5"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""h6"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    =================== Lists ============================================
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""ul"">
    <xs:annotation>
      <xs:documentation>
      Unordered list
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element maxOccurs=""unbounded"" ref=""li""/>
      </xs:sequence>
      <xs:attributeGroup ref=""attrs""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""ol"">
    <xs:annotation>
      <xs:documentation>
      Ordered (numbered) list
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element maxOccurs=""unbounded"" ref=""li""/>
      </xs:sequence>
      <xs:attributeGroup ref=""attrs""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""li"">
    <xs:annotation>
      <xs:documentation>
      list item
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Flow"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    definition lists - dt for term, dd for its definition
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""dl"">
    <xs:complexType>
      <xs:choice maxOccurs=""unbounded"">
        <xs:element ref=""dt""/>
        <xs:element ref=""dd""/>
      </xs:choice>
      <xs:attributeGroup ref=""attrs""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""dt"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""dd"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Flow"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    =================== Address ==========================================
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""address"">
    <xs:annotation>
      <xs:documentation>
      information on author
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    =================== Horizontal Rule ==================================
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""hr"">
    <xs:complexType>
      <xs:attributeGroup ref=""attrs""/>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    =================== Preformatted Text ================================
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""pre"">
    <xs:annotation>
      <xs:documentation>
      content is ""Inline"" excluding ""img|object|big|small|sub|sup""
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""pre.content"">
           <xs:attributeGroup ref=""attrs""/>
           <xs:attribute ref=""xml:space"" fixed=""preserve""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    =================== Block-like Quotes ================================
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""blockquote"">
    <xs:complexType>
      <xs:complexContent>
        <xs:extension base=""Block"">
          <xs:attributeGroup ref=""attrs""/>
          <xs:attribute name=""cite"" type=""URI""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    =================== Inserted/Deleted Text ============================

    ins/del are allowed in block and inline content, but its
    inappropriate to include block content within an ins element
    occurring in inline content.
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""ins"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Flow"">
          <xs:attributeGroup ref=""attrs""/>
          <xs:attribute name=""cite"" type=""URI""/>
          <xs:attribute name=""datetime"" type=""Datetime""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""del"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Flow"">
          <xs:attributeGroup ref=""attrs""/>
          <xs:attribute name=""cite"" type=""URI""/>
          <xs:attribute name=""datetime"" type=""Datetime""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    ================== The Anchor Element ================================
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""a"">
    <xs:annotation>
      <xs:documentation>
      content is ""Inline"" except that anchors shouldn't be nested
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""a.content"">
          <xs:attributeGroup ref=""attrs""/>
          <xs:attributeGroup ref=""focus""/>
          <xs:attribute name=""charset"" type=""Charset""/>
          <xs:attribute name=""type"" type=""ContentType""/>
          <xs:attribute name=""name"" type=""xs:NMTOKEN""/>
          <xs:attribute name=""href"" type=""URI""/>
          <xs:attribute name=""hreflang"" type=""LanguageCode""/>
          <xs:attribute name=""rel"" type=""LinkTypes""/>
          <xs:attribute name=""rev"" type=""LinkTypes""/>
          <xs:attribute name=""shape"" default=""rect"" type=""Shape""/>
          <xs:attribute name=""coords"" type=""Coords""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    ===================== Inline Elements ================================
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""span"">
    <xs:annotation>
      <xs:documentation>
      generic language/style container
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""bdo"">
    <xs:annotation>
      <xs:documentation>
      I18N BiDi over-ride
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""coreattrs""/>
          <xs:attributeGroup ref=""events""/>
          <xs:attribute name=""lang"" type=""LanguageCode""/>
          <xs:attribute ref=""xml:lang""/>
          <xs:attribute name=""dir"" use=""required"">
            <xs:simpleType>
              <xs:restriction base=""xs:token"">
                <xs:enumeration value=""ltr""/>
                <xs:enumeration value=""rtl""/>
              </xs:restriction>
            </xs:simpleType>
          </xs:attribute>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""br"">
    <xs:annotation>
      <xs:documentation>
      forced line break
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:attributeGroup ref=""coreattrs""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""em"">
    <xs:annotation>
      <xs:documentation>
      emphasis
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""strong"">
    <xs:annotation>
      <xs:documentation>
      strong emphasis
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""dfn"">
    <xs:annotation>
      <xs:documentation>
      definitional
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""code"">
    <xs:annotation>
      <xs:documentation>
      program code
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""samp"">
    <xs:annotation>
      <xs:documentation>
      sample
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""kbd"">
    <xs:annotation>
      <xs:documentation>
      something user would type
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""var"">
    <xs:annotation>
      <xs:documentation>
      variable
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""cite"">
    <xs:annotation>
      <xs:documentation>
      citation
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""abbr"">
    <xs:annotation>
      <xs:documentation>
      abbreviation
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""acronym"">
    <xs:annotation>
      <xs:documentation>
      acronym
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""q"">
    <xs:annotation>
      <xs:documentation>
      inlined quote
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
          <xs:attribute name=""cite"" type=""URI""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""sub"">
    <xs:annotation>
      <xs:documentation>
      subscript
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""sup"">
    <xs:annotation>
      <xs:documentation>
      superscript
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""tt"">
    <xs:annotation>
      <xs:documentation>
      fixed pitch font
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""i"">
    <xs:annotation>
      <xs:documentation>
      italic font
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""b"">
    <xs:annotation>
      <xs:documentation>
      bold font
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""big"">
    <xs:annotation>
      <xs:documentation>
      bigger font
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""small"">
    <xs:annotation>
      <xs:documentation>
      smaller font
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    ==================== Object ======================================

    object is used to embed objects as part of HTML pages.
    param elements should precede other content. Parameters
    can also be expressed as attribute/value pairs on the
    object element itself when brevity is desired.
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""object"">
    <xs:complexType mixed=""true"">
      <xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
        <xs:element ref=""param""/>
        <xs:group ref=""block""/>
        <xs:element ref=""form""/>
        <xs:group ref=""inline""/>
        <xs:group ref=""misc""/>
      </xs:choice>
      <xs:attributeGroup ref=""attrs""/>
      <xs:attribute name=""declare"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""declare""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute name=""classid"" type=""URI""/>
      <xs:attribute name=""codebase"" type=""URI""/>
      <xs:attribute name=""data"" type=""URI""/>
      <xs:attribute name=""type"" type=""ContentType""/>
      <xs:attribute name=""codetype"" type=""ContentType""/>
      <xs:attribute name=""archive"" type=""UriList""/>
      <xs:attribute name=""standby"" type=""Text""/>
      <xs:attribute name=""height"" type=""Length""/>
      <xs:attribute name=""width"" type=""Length""/>
      <xs:attribute name=""usemap"" type=""URI""/>
      <xs:attribute name=""name"" type=""xs:NMTOKEN""/>
      <xs:attribute name=""tabindex"" type=""tabindexNumber""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""param"">
    <xs:annotation>
      <xs:documentation>
      param is used to supply a named property value.
      In XML it would seem natural to follow RDF and support an
      abbreviated syntax where the param elements are replaced
      by attribute value pairs on the object start tag.
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:attribute name=""id"" type=""xs:ID""/>
      <xs:attribute name=""name""/>
      <xs:attribute name=""value""/>
      <xs:attribute name=""valuetype"" default=""data"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""data""/>
            <xs:enumeration value=""ref""/>
            <xs:enumeration value=""object""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute name=""type"" type=""ContentType""/>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    =================== Images ===========================================

    To avoid accessibility problems for people who aren't
    able to see the image, you should provide a text
    description using the alt and longdesc attributes.
    In addition, avoid the use of server-side image maps.
    Note that in this DTD there is no name attribute. That
    is only available in the transitional and frameset DTD.
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""img"">
    <xs:complexType>
      <xs:attributeGroup ref=""attrs""/>
      <xs:attribute name=""src"" use=""required"" type=""URI""/>
      <xs:attribute name=""alt"" use=""required"" type=""Text""/>
      <xs:attribute name=""longdesc"" type=""URI""/>
      <xs:attribute name=""height"" type=""Length""/>
      <xs:attribute name=""width"" type=""Length""/>
      <xs:attribute name=""usemap"" type=""URI"">
	<xs:annotation>
	  <xs:documentation>
          usemap points to a map element which may be in this document
          or an external document, although the latter is not widely supported
          </xs:documentation>
	</xs:annotation>
      </xs:attribute>
      <xs:attribute name=""ismap"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""ismap""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    ================== Client-side image maps ============================

    These can be placed in the same document or grouped in a
    separate document although this isn't yet widely supported
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""map"">
    <xs:complexType>
      <xs:choice>
        <xs:choice maxOccurs=""unbounded"">
          <xs:group ref=""block""/>
          <xs:element ref=""form""/>
          <xs:group ref=""misc""/>
        </xs:choice>
        <xs:element maxOccurs=""unbounded"" ref=""area""/>
      </xs:choice>
      <xs:attributeGroup ref=""i18n""/>
      <xs:attributeGroup ref=""events""/>
      <xs:attribute name=""id"" use=""required"" type=""xs:ID""/>
      <xs:attribute name=""class""/>
      <xs:attribute name=""style"" type=""StyleSheet""/>
      <xs:attribute name=""title"" type=""Text""/>
      <xs:attribute name=""name"" type=""xs:NMTOKEN""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""area"">
    <xs:complexType>
        <xs:attributeGroup ref=""attrs""/>
      <xs:attributeGroup ref=""focus""/>
      <xs:attribute name=""shape"" default=""rect"" type=""Shape""/>
      <xs:attribute name=""coords"" type=""Coords""/>
      <xs:attribute name=""href"" type=""URI""/>
      <xs:attribute name=""nohref"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""nohref""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute name=""alt"" use=""required"" type=""Text""/>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    ================ Forms ===============================================
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""form"">
    <xs:complexType>
      <xs:complexContent>
        <xs:extension base=""form.content"">
          <xs:attributeGroup ref=""attrs""/>
          <xs:attribute name=""action"" use=""required"" type=""URI""/>
          <xs:attribute name=""method"" default=""get"">
            <xs:simpleType>
              <xs:restriction base=""xs:token"">
                <xs:enumeration value=""get""/>
                <xs:enumeration value=""post""/>
              </xs:restriction>
            </xs:simpleType>
          </xs:attribute>
          <xs:attribute name=""enctype"" type=""ContentType""
              default=""application/x-www-form-urlencoded""/>
          <xs:attribute name=""onsubmit"" type=""Script""/>
          <xs:attribute name=""onreset"" type=""Script""/>
          <xs:attribute name=""accept"" type=""ContentTypes""/>
          <xs:attribute name=""accept-charset"" type=""Charsets""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""label"">
    <xs:annotation>
      <xs:documentation>
      Each label must not contain more than ONE field
      Label elements shouldn't be nested.
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
          <xs:attribute name=""for"" type=""xs:IDREF""/>
          <xs:attribute name=""accesskey"" type=""Character""/>
          <xs:attribute name=""onfocus"" type=""Script""/>
          <xs:attribute name=""onblur"" type=""Script""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:simpleType name=""InputType"">
    <xs:restriction base=""xs:token"">
      <xs:enumeration value=""text""/>
      <xs:enumeration value=""password""/>
      <xs:enumeration value=""checkbox""/>
      <xs:enumeration value=""radio""/>
      <xs:enumeration value=""submit""/>
      <xs:enumeration value=""reset""/>
      <xs:enumeration value=""file""/>
      <xs:enumeration value=""hidden""/>
      <xs:enumeration value=""image""/>
      <xs:enumeration value=""button""/>
    </xs:restriction>
  </xs:simpleType>

  <xs:element name=""input"">
    <xs:annotation>
      <xs:documentation>
      form control
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:attributeGroup ref=""attrs""/>
      <xs:attributeGroup ref=""focus""/>
      <xs:attribute name=""type"" default=""text"" type=""InputType""/>
      <xs:attribute name=""name"">
	<xs:annotation>
	  <xs:documentation>
          the name attribute is required for all but submit &amp; reset
          </xs:documentation>
	</xs:annotation>
      </xs:attribute>
      <xs:attribute name=""value""/>
      <xs:attribute name=""checked"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""checked""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute name=""disabled"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""disabled""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute name=""readonly"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""readonly""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute name=""size""/>
      <xs:attribute name=""maxlength"" type=""Number""/>
      <xs:attribute name=""src"" type=""URI""/>
      <xs:attribute name=""alt""/>
      <xs:attribute name=""usemap"" type=""URI""/>
      <xs:attribute name=""onselect"" type=""Script""/>
      <xs:attribute name=""onchange"" type=""Script""/>
      <xs:attribute name=""accept"" type=""ContentTypes""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""select"">
    <xs:annotation>
      <xs:documentation>
      option selector
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:choice maxOccurs=""unbounded"">
        <xs:element ref=""optgroup""/>
        <xs:element ref=""option""/>
      </xs:choice>
      <xs:attributeGroup ref=""attrs""/>
      <xs:attribute name=""name""/>
      <xs:attribute name=""size"" type=""Number""/>
      <xs:attribute name=""multiple"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""multiple""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute name=""disabled"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""disabled""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute name=""tabindex"" type=""tabindexNumber""/>
      <xs:attribute name=""onfocus"" type=""Script""/>
      <xs:attribute name=""onblur"" type=""Script""/>
      <xs:attribute name=""onchange"" type=""Script""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""optgroup"">
    <xs:annotation>
      <xs:documentation>
      option group
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element maxOccurs=""unbounded"" ref=""option""/>
      </xs:sequence>
      <xs:attributeGroup ref=""attrs""/>
      <xs:attribute name=""disabled"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""disabled""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute name=""label"" use=""required"" type=""Text""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""option"">
    <xs:annotation>
      <xs:documentation>
      selectable choice
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:attributeGroup ref=""attrs""/>
      <xs:attribute name=""selected"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""selected""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute name=""disabled"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""disabled""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute name=""label"" type=""Text""/>
      <xs:attribute name=""value""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""textarea"">
    <xs:annotation>
      <xs:documentation>
      multi-line text field
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:attributeGroup ref=""attrs""/>
      <xs:attributeGroup ref=""focus""/>
      <xs:attribute name=""name""/>
      <xs:attribute name=""rows"" use=""required"" type=""Number""/>
      <xs:attribute name=""cols"" use=""required"" type=""Number""/>
      <xs:attribute name=""disabled"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""disabled""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute name=""readonly"">
        <xs:simpleType>
          <xs:restriction base=""xs:token"">
            <xs:enumeration value=""readonly""/>
          </xs:restriction>
        </xs:simpleType>
      </xs:attribute>
      <xs:attribute name=""onselect"" type=""Script""/>
      <xs:attribute name=""onchange"" type=""Script""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""fieldset"">
    <xs:annotation>
      <xs:documentation>
      The fieldset element is used to group form fields.
      Only one legend element should occur in the content
      and if present should only be preceded by whitespace.

      NOTE: this content model is different from the XHTML 1.0 DTD,
      closer to the intended content model in HTML4 DTD
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:sequence>
        <xs:element ref=""legend""/>
        <xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
          <xs:group ref=""block""/>
          <xs:element ref=""form""/>
          <xs:group ref=""inline""/>
          <xs:group ref=""misc""/>
        </xs:choice>
      </xs:sequence>
      <xs:attributeGroup ref=""attrs""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""legend"">
    <xs:annotation>
      <xs:documentation>
      fieldset label
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
          <xs:attribute name=""accesskey"" type=""Character""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""button"">
    <xs:annotation>
      <xs:documentation>
      Content is ""Flow"" excluding a, form and form controls
      </xs:documentation>
    </xs:annotation>
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""button.content"">
          <xs:attributeGroup ref=""attrs""/>
          <xs:attributeGroup ref=""focus""/>
          <xs:attribute name=""name""/>
          <xs:attribute name=""value""/>
          <xs:attribute name=""type"" default=""submit"">
            <xs:simpleType>
              <xs:restriction base=""xs:token"">
                <xs:enumeration value=""button""/>
                <xs:enumeration value=""submit""/>
                <xs:enumeration value=""reset""/>
              </xs:restriction>
            </xs:simpleType>
          </xs:attribute>
          <xs:attribute name=""disabled"">
            <xs:simpleType>
              <xs:restriction base=""xs:token"">
                <xs:enumeration value=""disabled""/>
              </xs:restriction>
            </xs:simpleType>
          </xs:attribute>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    ======================= Tables =======================================

    Derived from IETF HTML table standard, see [RFC1942]
    </xs:documentation>
  </xs:annotation>

  <xs:simpleType name=""TFrame"">
    <xs:annotation>
      <xs:documentation>
      The border attribute sets the thickness of the frame around the
      table. The default units are screen pixels.

      The frame attribute specifies which parts of the frame around
      the table should be rendered. The values are not the same as
      CALS to avoid a name clash with the valign attribute.
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:token"">
      <xs:enumeration value=""void""/>
      <xs:enumeration value=""above""/>
      <xs:enumeration value=""below""/>
      <xs:enumeration value=""hsides""/>
      <xs:enumeration value=""lhs""/>
      <xs:enumeration value=""rhs""/>
      <xs:enumeration value=""vsides""/>
      <xs:enumeration value=""box""/>
      <xs:enumeration value=""border""/>
    </xs:restriction>
  </xs:simpleType>

  <xs:simpleType name=""TRules"">
    <xs:annotation>
      <xs:documentation>
      The rules attribute defines which rules to draw between cells:

      If rules is absent then assume:
          ""none"" if border is absent or border=""0"" otherwise ""all""
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:token"">
      <xs:enumeration value=""none""/>
      <xs:enumeration value=""groups""/>
      <xs:enumeration value=""rows""/>
      <xs:enumeration value=""cols""/>
      <xs:enumeration value=""all""/>
    </xs:restriction>
  </xs:simpleType>

  <xs:attributeGroup name=""cellhalign"">
    <xs:annotation>
      <xs:documentation>
      horizontal alignment attributes for cell contents

      char        alignment char, e.g. char=':'
      charoff     offset for alignment char
      </xs:documentation>
    </xs:annotation>
    <xs:attribute name=""align"">
      <xs:simpleType>
        <xs:restriction base=""xs:token"">
          <xs:enumeration value=""left""/>
          <xs:enumeration value=""center""/>
          <xs:enumeration value=""right""/>
          <xs:enumeration value=""justify""/>
          <xs:enumeration value=""char""/>
        </xs:restriction>
      </xs:simpleType>
    </xs:attribute>
    <xs:attribute name=""char"" type=""Character""/>
    <xs:attribute name=""charoff"" type=""Length""/>
  </xs:attributeGroup>

  <xs:attributeGroup name=""cellvalign"">
    <xs:annotation>
      <xs:documentation>
      vertical alignment attributes for cell contents
      </xs:documentation>
    </xs:annotation>
    <xs:attribute name=""valign"">
      <xs:simpleType>
        <xs:restriction base=""xs:token"">
          <xs:enumeration value=""top""/>
          <xs:enumeration value=""middle""/>
          <xs:enumeration value=""bottom""/>
          <xs:enumeration value=""baseline""/>
        </xs:restriction>
      </xs:simpleType>
    </xs:attribute>
  </xs:attributeGroup>

  <xs:element name=""table"">
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""0"" ref=""caption""/>
        <xs:choice>
          <xs:element minOccurs=""0"" maxOccurs=""unbounded"" ref=""col""/>
          <xs:element minOccurs=""0"" maxOccurs=""unbounded"" ref=""colgroup""/>
        </xs:choice>
        <xs:element minOccurs=""0"" ref=""thead""/>
        <xs:element minOccurs=""0"" ref=""tfoot""/>
        <xs:choice>
          <xs:element maxOccurs=""unbounded"" ref=""tbody""/>
          <xs:element maxOccurs=""unbounded"" ref=""tr""/>
        </xs:choice>
      </xs:sequence>
      <xs:attributeGroup ref=""attrs""/>
      <xs:attribute name=""summary"" type=""Text""/>
      <xs:attribute name=""width"" type=""Length""/>
      <xs:attribute name=""border"" type=""Pixels""/>
      <xs:attribute name=""frame"" type=""TFrame""/>
      <xs:attribute name=""rules"" type=""TRules""/>
      <xs:attribute name=""cellspacing"" type=""Length""/>
      <xs:attribute name=""cellpadding"" type=""Length""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""caption"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Inline"">
          <xs:attributeGroup ref=""attrs""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:annotation>
    <xs:documentation>
    Use thead to duplicate headers when breaking table
    across page boundaries, or for static headers when
    tbody sections are rendered in scrolling panel.

    Use tfoot to duplicate footers when breaking table
    across page boundaries, or for static footers when
    tbody sections are rendered in scrolling panel.

    Use multiple tbody sections when rules are needed
    between groups of table rows.
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""thead"">
    <xs:complexType>
      <xs:sequence>
        <xs:element maxOccurs=""unbounded"" ref=""tr""/>
      </xs:sequence>
      <xs:attributeGroup ref=""attrs""/>
      <xs:attributeGroup ref=""cellhalign""/>
      <xs:attributeGroup ref=""cellvalign""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""tfoot"">
    <xs:complexType>
      <xs:sequence>
        <xs:element maxOccurs=""unbounded"" ref=""tr""/>
      </xs:sequence>
      <xs:attributeGroup ref=""attrs""/>
      <xs:attributeGroup ref=""cellhalign""/>
      <xs:attributeGroup ref=""cellvalign""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""tbody"">
    <xs:complexType>
      <xs:sequence>
        <xs:element maxOccurs=""unbounded"" ref=""tr""/>
      </xs:sequence>
      <xs:attributeGroup ref=""attrs""/>
      <xs:attributeGroup ref=""cellhalign""/>
      <xs:attributeGroup ref=""cellvalign""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""colgroup"">
    <xs:annotation>
      <xs:documentation>
      colgroup groups a set of col elements. It allows you to group
      several semantically related columns together.
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""0"" maxOccurs=""unbounded"" ref=""col""/>
      </xs:sequence>
      <xs:attributeGroup ref=""attrs""/>
      <xs:attribute name=""span"" default=""1"" type=""Number""/>
      <xs:attribute name=""width"" type=""MultiLength""/>
      <xs:attributeGroup ref=""cellhalign""/>
      <xs:attributeGroup ref=""cellvalign""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""col"">
    <xs:annotation>
      <xs:documentation>
      col elements define the alignment properties for cells in
      one or more columns.

      The width attribute specifies the width of the columns, e.g.

          width=64        width in screen pixels
          width=0.5*      relative width of 0.5

      The span attribute causes the attributes of one
      col element to apply to more than one column.
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:attributeGroup ref=""attrs""/>
      <xs:attribute name=""span"" default=""1"" type=""Number""/>
      <xs:attribute name=""width"" type=""MultiLength""/>
      <xs:attributeGroup ref=""cellhalign""/>
      <xs:attributeGroup ref=""cellvalign""/>
    </xs:complexType>
  </xs:element>

  <xs:element name=""tr"">
    <xs:complexType>
      <xs:choice maxOccurs=""unbounded"">
        <xs:element ref=""th""/>
        <xs:element ref=""td""/>
      </xs:choice>
      <xs:attributeGroup ref=""attrs""/>
      <xs:attributeGroup ref=""cellhalign""/>
      <xs:attributeGroup ref=""cellvalign""/>
    </xs:complexType>
  </xs:element>

  <xs:simpleType name=""Scope"">
    <xs:annotation>
      <xs:documentation>
      Scope is simpler than headers attribute for common tables
      </xs:documentation>
    </xs:annotation>
    <xs:restriction base=""xs:token"">
      <xs:enumeration value=""row""/>
      <xs:enumeration value=""col""/>
      <xs:enumeration value=""rowgroup""/>
      <xs:enumeration value=""colgroup""/>
    </xs:restriction>
  </xs:simpleType>

  <xs:annotation>
    <xs:documentation>
    th is for headers, td for data and for cells acting as both
    </xs:documentation>
  </xs:annotation>

  <xs:element name=""th"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Flow"">
          <xs:attributeGroup ref=""attrs""/>
          <xs:attribute name=""abbr"" type=""Text""/>
          <xs:attribute name=""axis""/>
          <xs:attribute name=""headers"" type=""xs:IDREFS""/>
          <xs:attribute name=""scope"" type=""Scope""/>
          <xs:attribute name=""rowspan"" default=""1"" type=""Number""/>
          <xs:attribute name=""colspan"" default=""1"" type=""Number""/>
          <xs:attributeGroup ref=""cellhalign""/>
          <xs:attributeGroup ref=""cellvalign""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

  <xs:element name=""td"">
    <xs:complexType mixed=""true"">
      <xs:complexContent>
        <xs:extension base=""Flow"">
          <xs:attributeGroup ref=""attrs""/>
          <xs:attribute name=""abbr"" type=""Text""/>
          <xs:attribute name=""axis""/>
          <xs:attribute name=""headers"" type=""xs:IDREFS""/>
          <xs:attribute name=""scope"" type=""Scope""/>
          <xs:attribute name=""rowspan"" default=""1"" type=""Number""/>
          <xs:attribute name=""colspan"" default=""1"" type=""Number""/>
          <xs:attributeGroup ref=""cellhalign""/>
          <xs:attributeGroup ref=""cellvalign""/>
        </xs:extension>
      </xs:complexContent>
    </xs:complexType>
  </xs:element>

</xs:schema>
";
        #endregion

        private static readonly XmlSchema Schema;

        private readonly XmlDocument xmlDocument;
        private bool isValid;

        static XmlStrictValidation()
        {
            var reader = new StringReader(SchemaString);
            Schema = XmlSchema.Read(reader, null);
        }

        public XmlStrictValidation(string html)
        {
            this.isValid = true;

            var settings = new XmlReaderSettings
                {
                    ValidationType = ValidationType.Schema,
                    ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings,
                    DtdProcessing = DtdProcessing.Prohibit
                };
            settings.ValidationEventHandler += this.ValidationCallBack;
            settings.Schemas.Add(Schema);

            var stringReader = new StringReader(html);
            var xmlreader = XmlReader.Create(stringReader, settings);
            
            this.xmlDocument = new XmlDocument();
            this.XmlDocument.Load(xmlreader);
        }

        public bool IsValid
        {
            get { return this.isValid; }
        }

        public XmlDocument XmlDocument
        {
            get { return this.xmlDocument; }
        }

        private void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            this.isValid = false;
        }
    }
}