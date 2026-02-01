#!/usr/bin/env node

/**
 * MCP Server for Magic Car Repair AI
 * This server connects to Stitch (Google's MCP service) and provides
 * access to Magic Car Repair application features
 */

import { Server } from '@modelcontextprotocol/sdk/server/index.js';
import { StdioServerTransport } from '@modelcontextprotocol/sdk/server/stdio.js';
import {
  CallToolRequestSchema,
  ListToolsRequestSchema,
  ListResourcesRequestSchema,
  ReadResourceRequestSchema,
} from '@modelcontextprotocol/sdk/types.js';

// Configuration
const API_BASE_URL = process.env.API_BASE_URL || 'http://localhost:5000';
const API_TOKEN = process.env.API_TOKEN || '';

/**
 * Initialize the MCP Server
 */
const server = new Server(
  {
    name: 'magic-car-repair-mcp-server',
    version: '1.0.0',
  },
  {
    capabilities: {
      tools: {},
      resources: {},
    },
  }
);

/**
 * List available tools
 */
server.setRequestHandler(ListToolsRequestSchema, async () => {
  return {
    tools: [
      {
        name: 'get_work_orders',
        description: 'Get work orders from the Magic Car Repair system',
        inputSchema: {
          type: 'object',
          properties: {
            status: {
              type: 'string',
              description: 'Filter by work order status',
              enum: ['Pending', 'InProgress', 'Completed', 'Cancelled'],
            },
            customerId: {
              type: 'number',
              description: 'Filter by customer ID',
            },
            limit: {
              type: 'number',
              description: 'Maximum number of results',
              default: 10,
            },
          },
        },
      },
      {
        name: 'get_customer_info',
        description: 'Get customer information by ID',
        inputSchema: {
          type: 'object',
          properties: {
            customerId: {
              type: 'number',
              description: 'Customer ID',
              required: true,
            },
          },
          required: ['customerId'],
        },
      },
      {
        name: 'get_vehicle_info',
        description: 'Get vehicle information by ID',
        inputSchema: {
          type: 'object',
          properties: {
            vehicleId: {
              type: 'number',
              description: 'Vehicle ID',
              required: true,
            },
          },
          required: ['vehicleId'],
        },
      },
      {
        name: 'create_appointment',
        description: 'Create a new appointment',
        inputSchema: {
          type: 'object',
          properties: {
            customerId: {
              type: 'number',
              description: 'Customer ID',
              required: true,
            },
            vehicleId: {
              type: 'number',
              description: 'Vehicle ID',
              required: true,
            },
            appointmentDate: {
              type: 'string',
              description: 'Appointment date (ISO 8601 format)',
              required: true,
            },
            description: {
              type: 'string',
              description: 'Appointment description',
            },
          },
          required: ['customerId', 'vehicleId', 'appointmentDate'],
        },
      },
      {
        name: 'get_ai_diagnosis',
        description: 'Get AI-powered diagnosis for a vehicle issue',
        inputSchema: {
          type: 'object',
          properties: {
            vehicleId: {
              type: 'number',
              description: 'Vehicle ID',
              required: true,
            },
            symptoms: {
              type: 'string',
              description: 'Description of the vehicle symptoms',
              required: true,
            },
          },
          required: ['vehicleId', 'symptoms'],
        },
      },
      {
        name: 'get_parts_inventory',
        description: 'Search for parts in inventory',
        inputSchema: {
          type: 'object',
          properties: {
            searchTerm: {
              type: 'string',
              description: 'Search term for part name or code',
            },
            category: {
              type: 'string',
              description: 'Part category',
            },
            inStock: {
              type: 'boolean',
              description: 'Filter by stock availability',
            },
          },
        },
      },
    ],
  };
});

/**
 * Handle tool calls
 */
server.setRequestHandler(CallToolRequestSchema, async (request) => {
  const { name, arguments: args } = request.params;

  try {
    switch (name) {
      case 'get_work_orders':
        return await handleGetWorkOrders(args);
      
      case 'get_customer_info':
        return await handleGetCustomerInfo(args);
      
      case 'get_vehicle_info':
        return await handleGetVehicleInfo(args);
      
      case 'create_appointment':
        return await handleCreateAppointment(args);
      
      case 'get_ai_diagnosis':
        return await handleGetAIDiagnosis(args);
      
      case 'get_parts_inventory':
        return await handleGetPartsInventory(args);
      
      default:
        throw new Error(`Unknown tool: ${name}`);
    }
  } catch (error) {
    return {
      content: [
        {
          type: 'text',
          text: `Error: ${error.message}`,
        },
      ],
      isError: true,
    };
  }
});

/**
 * List available resources
 */
server.setRequestHandler(ListResourcesRequestSchema, async () => {
  return {
    resources: [
      {
        uri: 'magic-car-repair://api/health',
        name: 'API Health Check',
        description: 'Check the health status of the Magic Car Repair API',
        mimeType: 'application/json',
      },
      {
        uri: 'magic-car-repair://api/stats',
        name: 'System Statistics',
        description: 'Get system statistics and metrics',
        mimeType: 'application/json',
      },
    ],
  };
});

/**
 * Read resource content
 */
server.setRequestHandler(ReadResourceRequestSchema, async (request) => {
  const { uri } = request.params;

  try {
    if (uri.startsWith('magic-car-repair://api/')) {
      const path = uri.replace('magic-car-repair://api/', '');
      const response = await fetch(`${API_BASE_URL}/api/${path}`, {
        headers: {
          'Authorization': `Bearer ${API_TOKEN}`,
          'Content-Type': 'application/json',
        },
      });

      if (!response.ok) {
        throw new Error(`API request failed: ${response.statusText}`);
      }

      const data = await response.json();
      return {
        contents: [
          {
            uri,
            mimeType: 'application/json',
            text: JSON.stringify(data, null, 2),
          },
        ],
      };
    }

    throw new Error(`Unknown resource: ${uri}`);
  } catch (error) {
    return {
      contents: [
        {
          uri,
          mimeType: 'text/plain',
          text: `Error: ${error.message}`,
        },
      ],
    };
  }
});

// Tool handlers

async function handleGetWorkOrders(args) {
  const params = new URLSearchParams();
  if (args.status) params.append('status', args.status);
  if (args.customerId) params.append('customerId', args.customerId);
  if (args.limit) params.append('limit', args.limit);

  const response = await fetch(`${API_BASE_URL}/api/workorders?${params}`, {
    headers: {
      'Authorization': `Bearer ${API_TOKEN}`,
      'Content-Type': 'application/json',
    },
  });

  if (!response.ok) {
    throw new Error(`Failed to fetch work orders: ${response.statusText}`);
  }

  const data = await response.json();
  return {
    content: [
      {
        type: 'text',
        text: JSON.stringify(data, null, 2),
      },
    ],
  };
}

async function handleGetCustomerInfo(args) {
  const response = await fetch(`${API_BASE_URL}/api/customers/${args.customerId}`, {
    headers: {
      'Authorization': `Bearer ${API_TOKEN}`,
      'Content-Type': 'application/json',
    },
  });

  if (!response.ok) {
    throw new Error(`Failed to fetch customer info: ${response.statusText}`);
  }

  const data = await response.json();
  return {
    content: [
      {
        type: 'text',
        text: JSON.stringify(data, null, 2),
      },
    ],
  };
}

async function handleGetVehicleInfo(args) {
  const response = await fetch(`${API_BASE_URL}/api/vehicles/${args.vehicleId}`, {
    headers: {
      'Authorization': `Bearer ${API_TOKEN}`,
      'Content-Type': 'application/json',
    },
  });

  if (!response.ok) {
    throw new Error(`Failed to fetch vehicle info: ${response.statusText}`);
  }

  const data = await response.json();
  return {
    content: [
      {
        type: 'text',
        text: JSON.stringify(data, null, 2),
      },
    ],
  };
}

async function handleCreateAppointment(args) {
  const response = await fetch(`${API_BASE_URL}/api/appointments`, {
    method: 'POST',
    headers: {
      'Authorization': `Bearer ${API_TOKEN}`,
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      customerId: args.customerId,
      vehicleId: args.vehicleId,
      appointmentDate: args.appointmentDate,
      description: args.description || '',
    }),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Failed to create appointment: ${errorText}`);
  }

  const data = await response.json();
  return {
    content: [
      {
        type: 'text',
        text: JSON.stringify(data, null, 2),
      },
    ],
  };
}

async function handleGetAIDiagnosis(args) {
  const response = await fetch(`${API_BASE_URL}/api/ai/diagnosis`, {
    method: 'POST',
    headers: {
      'Authorization': `Bearer ${API_TOKEN}`,
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      vehicleId: args.vehicleId,
      symptoms: args.symptoms,
    }),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Failed to get AI diagnosis: ${errorText}`);
  }

  const data = await response.json();
  return {
    content: [
      {
        type: 'text',
        text: JSON.stringify(data, null, 2),
      },
    ],
  };
}

async function handleGetPartsInventory(args) {
  const params = new URLSearchParams();
  if (args.searchTerm) params.append('search', args.searchTerm);
  if (args.category) params.append('category', args.category);
  if (args.inStock !== undefined) params.append('inStock', args.inStock);

  const response = await fetch(`${API_BASE_URL}/api/parts?${params}`, {
    headers: {
      'Authorization': `Bearer ${API_TOKEN}`,
      'Content-Type': 'application/json',
    },
  });

  if (!response.ok) {
    throw new Error(`Failed to fetch parts inventory: ${response.statusText}`);
  }

  const data = await response.json();
  return {
    content: [
      {
        type: 'text',
        text: JSON.stringify(data, null, 2),
      },
    ],
  };
}

/**
 * Start the server
 */
async function main() {
  const transport = new StdioServerTransport();
  await server.connect(transport);
  console.error('Magic Car Repair MCP Server running on stdio');
}

main().catch((error) => {
  console.error('Fatal error in main():', error);
  process.exit(1);
});
